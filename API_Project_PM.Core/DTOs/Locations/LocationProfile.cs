using API_Project_PM.Core.Models;
using AutoMapper;

namespace API_Project_PM.Core.DTOs.Locations
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<Location, LocationDto>();
            CreateMap<Location, CreateLocationDto>().ReverseMap();
            CreateMap<Location, UpdateLocationDto>().ReverseMap();
        }

    }
}
