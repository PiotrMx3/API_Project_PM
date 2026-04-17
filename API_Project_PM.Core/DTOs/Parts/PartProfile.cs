using API_Project_PM.Core.Models;
using AutoMapper;

namespace API_Project_PM.Core.DTOs.Parts
{
    public class PartProfile : Profile
    {

        public PartProfile()
        {
            CreateMap<Part, PartDto>();
            CreateMap<Part, CreatePartDto>().ReverseMap();
            CreateMap<Part, UpdatePartDto>().ReverseMap();
        }
    }
}
