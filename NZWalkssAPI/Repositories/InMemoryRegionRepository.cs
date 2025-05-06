//this class is
//
//for demonstration purposed / intrest sake

using NZWalkssAPI.Models.Domain;
namespace NZWalkssAPI.Repositories
{
    public class InMemoryRegionRepository : IRegionRepository
    {
        public Task<Region> CreateAsync(Region region)
        {
            throw new NotImplementedException();
        }

        public Task<Region?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Region>> GetAllAsync()
        {
            return Task.FromResult(new List<Region>

            {
                new Region()
                {
                    Id = Guid.NewGuid(),
                    Code = "CNT",
                    Name = "Centurion",

                 // Commented it out because this field allows null values so its not important for it to be passed.
                 // RegionImageURL = "https://centurion.net.au/wp-content/uploads/2018/10/Map-300x210.jpg"
                }
            });
        }

        public Task<Region?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Region?> UpdateAsync(Guid id, Region region)
        {
            throw new NotImplementedException();
        }
    }
}
