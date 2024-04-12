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
    public class CampRepository : ICampRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CampRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Camp> GetCampByIdAsync(int campId)
        {
            return await _dbContext.Camps.FindAsync(campId);
        }

        public async Task<IEnumerable<Camp>> GetAllCampsAsync()
        {
            return await _dbContext.Camps.Where(b => b.IsBooked == true).ToListAsync();
        }

        public async Task AddCampAsync(Camp camp)
        {
            _dbContext.Camps.Add(camp);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCampAsync(Camp camp)
        {
            _dbContext.Camps.Update(camp);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCampAsync(int campId)
        {
            var camp = await _dbContext.Camps.FindAsync(campId);
            if (camp != null)
            {
                _dbContext.Camps.Remove(camp);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
