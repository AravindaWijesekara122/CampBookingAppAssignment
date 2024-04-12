using BusinessLogicLayer.DTO;
using BusinessLogicLayer.IService;
using DataAccessLayer.Entities;
using DataAccessLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICampRepository _campRepository;

        public BookingService(IBookingRepository bookingRepository, ICampRepository campRepository)
        {
            _bookingRepository = bookingRepository;
            _campRepository = campRepository;
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int userId)
        {
            return await _bookingRepository.GetBookingsByUserIdAsync(userId);
        }

        public async Task<IEnumerable<Booking>> GetBookingsByCampIdAsync(int campId)
        {
            return await _bookingRepository.GetBookingsByCampIdAsync(campId);
        }

        public async Task<Booking> GetBookingByRefNumberAsync(string refNumber)
        {
            return await _bookingRepository.GetBookingByRefNumberAsync(refNumber);
        }

        public async Task MakeBookingAsync(BookingRequestDTO requestDTO) //int userId, int campId, DateTime checkInDate, DateTime checkOutDate)
        {
            // Check if the camp exists
            var camp = await _campRepository.GetCampByIdAsync(requestDTO.CampId);
            if (camp == null)
            {
                throw new Exception("Camp not found");
            }
            if (camp.Capacity < requestDTO.Capacity)
            {
                throw new Exception("Camp Capacity exceeded");
            }
            camp.Capacity = requestDTO.Capacity;

            // Check if the camp is available for the selected dates
            if (!await IsCampAvailableAsync(requestDTO.CampId, requestDTO.CheckInDate, requestDTO.CheckOutDate))
            {
                throw new Exception("The camp is not available for the selected dates");
            }

            var user = new User
            {
                Name = requestDTO.Name,
                Address = requestDTO.Address,
                State = requestDTO.State,
                Country = requestDTO.Country,
                ZipCode = requestDTO.ZipCode,
                PhoneNo = requestDTO.PhoneNo,
            };


            // Calculate total cost
            int totalCost = CalculateTotalCost(requestDTO.CheckInDate, requestDTO.CheckOutDate, camp.RatePerNight);

            // Generate booking reference number
            string bookingReferenceNumber = GenerateBookingReferenceNumber();

            // Create new booking
            var booking = new Booking
            {
                UserId = user.UserId,
                CampId = requestDTO.CampId,
                CheckInDate = requestDTO.CheckInDate,
                CheckOutDate = requestDTO.CheckOutDate,
                TotalCost = totalCost,
                BookingReferenceNumber = bookingReferenceNumber,
                User = user,
                Camp = camp,
            };

            camp.IsBooked = true;

            // Add booking to the database
            await _bookingRepository.AddBookingAsync(booking);

            // Mark the camp as booked for the selected dates
            //await MarkCampAsBookedAsync(requestDTO.CampId, requestDTO.CheckInDate, requestDTO.CheckOutDate);
        }

        private int CalculateTotalCost(DateTime checkInDate, DateTime checkOutDate, int ratePerNight)
        {
            // Calculate total cost based on the number of nights
            int numberOfNights = (int)(checkOutDate - checkInDate).TotalDays;
            return numberOfNights * ratePerNight;
        }

        private async Task<bool> IsCampAvailableAsync(int campId, DateTime checkInDate, DateTime checkOutDate)
        {
            var bookings = await _bookingRepository.GetBookingsByCampIdAsync(campId);
            return !bookings.Any(b => !(checkOutDate <= b.CheckInDate || checkInDate >= b.CheckOutDate));
        }

        private async Task MarkCampAsBookedAsync(int campId, DateTime checkInDate, DateTime checkOutDate)
        {
            var camp = await _campRepository.GetCampByIdAsync(campId);
            if (camp != null)
            {
                // Update camp availability status for the selected dates
                // Implementation depends on how camp availability is stored in the database
                // Example: camp.IsAvailable = false;
                // Update the camp in the database
                await _campRepository.UpdateCampAsync(camp);
            }
        }

        private string GenerateBookingReferenceNumber()
        {
            // Generate a unique 8-digit alpha-numeric code as the booking reference number
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
