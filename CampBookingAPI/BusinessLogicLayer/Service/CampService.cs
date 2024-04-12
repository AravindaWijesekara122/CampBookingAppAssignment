using BusinessLogicLayer.IService;
using DataAccessLayer.Entities;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public class CampService : ICampService
    {
        private readonly ICampRepository _campRepository;
        private readonly IBookingRepository _bookingRepository;

        public CampService(ICampRepository campRepository, IBookingRepository bookingRepository)
        {
            _campRepository = campRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<Camp> GetCampByIdAsync(int campId)
        {
            return await _campRepository.GetCampByIdAsync(campId);
        }

        public async Task<IEnumerable<Camp>> GetAllCampsAsync()
        {
            return await _campRepository.GetAllCampsAsync();
        }

        public async Task<IEnumerable<Camp>> GetAvailableCampsAsync(DateTime checkInDate, DateTime checkOutDate)
        {
            var allCamps = await _campRepository.GetAllCampsAsync();
            var bookedCampIds = await _bookingRepository.GetBookedCampIdsAsync(checkInDate, checkOutDate);

            var notBookedCamps = allCamps.Where(c => !c.IsBooked);

            var availableCamps = allCamps.Where(c => !bookedCampIds.Contains(c.CampId));
                                            
            return availableCamps.Concat(notBookedCamps);
        }

        public async Task AddCampAsync(Camp camp)
        {
            await _campRepository.AddCampAsync(camp);
        }

        public async Task UpdateCampAsync(Camp camp)
        {
            await _campRepository.UpdateCampAsync(camp);
        }

        public async Task DeleteCampAsync(int campId)
        {
            await _campRepository.DeleteCampAsync(campId);
        }
    }
}
