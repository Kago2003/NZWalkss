using AutoMapper;
using NZWalkssAPI.Models.Domain;
using NZWalkssAPI.Models.DTO;

namespace NZWalkssAPI.Mappings
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddRegionRequestDTO, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDTO, Region>().ReverseMap();
            CreateMap<AddWalksRequestDTO, Walks>().ReverseMap();
            CreateMap<Walks, WalksDTO>().ReverseMap();
            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
            CreateMap<UpdateWalkRequestDTO, Walks>().ReverseMap();
        }
    }
}
