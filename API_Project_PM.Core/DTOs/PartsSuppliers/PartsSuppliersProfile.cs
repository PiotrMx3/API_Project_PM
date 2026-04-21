using API_Project_PM.Core.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Project_PM.Core.DTOs.PartsSuppliers
{
    public class PartsSuppliersProfile : Profile
    {
        public PartsSuppliersProfile()
        {
            CreateMap<PartSupplier, CreatePartSupplierDto>().ReverseMap();
            CreateMap<PartSupplier, UpdatePartSupplierDto>().ReverseMap();
            CreateMap<PartSupplier, PartSupplierDto>()
                .ForMember(s => s.partName, opt => opt.MapFrom(s => s.Part.Name))
                .ForMember(s => s.supplierName, opt => opt.MapFrom(s => s.Supplier.Name));
            
        }



    }
}
