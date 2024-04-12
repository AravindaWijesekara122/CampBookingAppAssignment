using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface ICampRepository
    {
        Task<Camp> GetCampByIdAsync(int campId);
        Task<IEnumerable<Camp>> GetAllCampsAsync();
        Task AddCampAsync(Camp camp);
        Task UpdateCampAsync(Camp camp);
        Task DeleteCampAsync(int campId);
    }
}
