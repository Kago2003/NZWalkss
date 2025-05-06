using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalkssAPI.Data;
using NZWalkssAPI.Models.Domain;
using System.Linq;
using System.Runtime.InteropServices;

namespace NZWalkssAPI.Repositories
{
    public class SQLWalksRepository : IWalksRepository
    {
        private readonly NZWalkssDbContext dbContext;

        public SQLWalksRepository(NZWalkssDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }
        public async Task<Walks> CreateAsync(Walks walks)
        {
            await dbContext.Walks.AddAsync(walks);
            await dbContext.SaveChangesAsync();
            return walks;
        }

        public async Task<List<Walks>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1,  int pageSize = 1000)
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //Filtering
            if (string.IsNullOrEmpty(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            { 
                //if the name contains any of the results that we are passing, then we want a filtered result inside the variable
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
                
            }

            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) ==  false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name): walks.OrderByDescending(x=> x.Name);
                }
                else if(sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKM): walks.OrderByDescending(walks=> walks.LengthInKM);
                }
            }

            //Pagination 
            var skipResults = (pageNumber - 1) * pageSize;


            return await walks.Skip(skipResults).Take(pageNumber).ToListAsync();

           //return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walks?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walks?> UpdateAsync(Guid id, Walks walks)
        {
            var existingwalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingwalk == null) 
            {  
                return null; 
            }

            existingwalk.Name = walks.Name;
            existingwalk.Description = walks.Description;
            existingwalk.LengthInKM = walks.LengthInKM;
            existingwalk.WalkImageURL = walks.WalkImageURL;
            existingwalk.DifficultyId = walks.DifficultyId;
            existingwalk.RegionId = walks.RegionId;

            await dbContext.SaveChangesAsync();
            return existingwalk;
        }

        public async Task<Walks?> DeleteAsync(Guid id)
        {
           var existingwalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingwalk == null)
            {
                return null;
            }

            dbContext.Walks.Remove(existingwalk);
            await dbContext.SaveChangesAsync();
            return existingwalk;
        }
    }
}