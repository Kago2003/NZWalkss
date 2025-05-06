using NZWalkssAPI.Models.Domain;

namespace NZWalkssAPI.Repositories
{
    public interface IWalksRepository
    {
        Task <Walks> CreateAsync(Walks walks);
        Task<List<Walks>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 0, int pageSize = 1000);
        Task<Walks?> GetByIdAsync(Guid id);
        Task<Walks?> UpdateAsync(Guid id, Walks walks);
        Task <Walks?> DeleteAsync(Guid id);
    }
}
