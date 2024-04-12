using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int userId);
        Task<IEnumerable<Booking>> GetBookingsByCampIdAsync(int campId);
        Task<Booking> GetBookingByRefNumberAsync(string refNumber);
        Task<IEnumerable<int>> GetBookedCampIdsAsync(DateTime checkInDate, DateTime checkOutDate);
        Task AddBookingAsync(Booking booking);
        Task UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(int bookingId);
    }
}

