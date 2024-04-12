using DataAccessLayer.Entities;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BookingRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int userId)
        {
            return await _dbContext.Bookings.Where(b => b.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsByCampIdAsync(int campId)
        {
            return await _dbContext.Bookings.Where(b => b.CampId == campId).ToListAsync();
        }

        public async Task<Booking> GetBookingByRefNumberAsync(string refNumber)
        {
            return await _dbContext.Bookings.FirstOrDefaultAsync(b => b.BookingReferenceNumber.Equals(refNumber));
        }

        public async Task<IEnumerable<int>> GetBookedCampIdsAsync(DateTime checkInDate, DateTime checkOutDate)
        {
            return await _dbContext.Bookings
                .Where(b => b.CheckInDate <= checkOutDate && b.CheckOutDate >= checkInDate)
                .Select(b => b.CampId)
                .ToListAsync();
        }

        public async Task AddBookingAsync(Booking booking)
        {
            _dbContext.Bookings.Add(booking);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            _dbContext.Bookings.Update(booking);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(int bookingId)
        {
            var booking = await _dbContext.Bookings.FindAsync(bookingId);
            if (booking != null)
            {
                _dbContext.Bookings.Remove(booking);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
