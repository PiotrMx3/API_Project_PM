using API_Project_PM.Core.Models;
using AutoMapper;

namespace API_Project_PM.Core.DTOs.Suppliers
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<Supplier, SupplierDto>();
            CreateMap<Supplier, CreateSupplierDto>().ReverseMap();
            CreateMap<Supplier, UpdateSupplierDto>().ReverseMap();
            CreateMap<Supplier, SupplierWithPartsDto>()
                .ForMember(d => d.Parts , opt => opt.MapFrom(s => s.PartSuppliers.Select(ps => ps.Part)));


        }

    }
}
