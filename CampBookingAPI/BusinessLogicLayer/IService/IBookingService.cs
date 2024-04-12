using BusinessLogicLayer.DTO;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IService
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int userId);
        Task<IEnumerable<Booking>> GetBookingsByCampIdAsync(int campId);
        Task<Booking> GetBookingByRefNumberAsync(string refNumber);
        Task MakeBookingAsync(BookingRequestDTO requestDTO);
    }
}
