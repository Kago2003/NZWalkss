using NZWalkssAPI.Models.Domain;

namespace NZWalkssAPI.Repositories
{
    public interface IImageRepository
    {
        Task <Image> Upload(Image image);
    }
}
