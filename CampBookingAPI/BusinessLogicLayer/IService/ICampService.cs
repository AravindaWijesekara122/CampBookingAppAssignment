using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IService
{
    public interface ICampService
    {
        Task<Camp> GetCampByIdAsync(int campId);
        Task<IEnumerable<Camp>> GetAllCampsAsync();
        Task<IEnumerable<Camp>> GetAvailableCampsAsync(DateTime checkInDate, DateTime checkOutDate);
        Task AddCampAsync(Camp camp);
        Task UpdateCampAsync(Camp camp);
        Task DeleteCampAsync(int campId);
    }
}
