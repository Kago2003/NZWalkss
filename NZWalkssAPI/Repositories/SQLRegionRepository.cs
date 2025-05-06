using Microsoft.EntityFrameworkCore;
using NZWalkssAPI.Data;
using NZWalkssAPI.Models.Domain;
using NZWalkssAPI.Models.DTO;

namespace NZWalkssAPI.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalkssDbContext dbContext;

        //injecting the dbcontext class 
        public SQLRegionRepository(NZWalkssDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Reagions.ToListAsync();
        }

         public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Reagions.FindAsync(id);
        }

         public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Reagions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingReagion = await dbContext.Reagions.FindAsync(id);
            if (existingReagion == null) 
            {
                return null;
            }

            existingReagion.Code = region.Code;
            existingReagion.Name = region.Name;
            existingReagion.RegionImageURL = region.RegionImageURL;

            await dbContext.SaveChangesAsync();
            return existingReagion;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingReagion = await dbContext.Reagions.FindAsync(id);
            if (existingReagion == null)
            {
                return null;
            }

            dbContext.Reagions.Remove(existingReagion);
            await dbContext.SaveChangesAsync();
            return existingReagion;
        }
    }
}
