using API_Project_PM.Core.Models;
using AutoMapper;

namespace API_Project_PM.Core.DTOs.Parts
{
    public class PartProfile : Profile
    {

        public PartProfile()
        {
            CreateMap<Part, PartDto>()
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category!.Name))
                .ForMember(d => d.DefaultLocation, opt => opt.MapFrom(s => s.DefaultLocation == null ? "Geen Locatie" : $"{s.DefaultLocation!.Zone}-{s.DefaultLocation.Rack}-{s.DefaultLocation.Shelf}-{s.DefaultLocation.Box}"));
            CreateMap<Part, CreatePartDto>().ReverseMap();
            CreateMap<Part, UpdatePartDto>().ReverseMap();
        }
    }
}
