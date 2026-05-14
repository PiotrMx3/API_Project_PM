using API_Project_PM.Core.Models;
using AutoMapper;

namespace API_Project_PM.Core.DTOs.StockItems
{
    public class StockItemProfile : Profile
    {
        public StockItemProfile()
        {
            CreateMap<StockItem, StockItemDto>()
                .ForMember(d => d.PartLocation, opt => opt.MapFrom(s => s.Location == null ? "Geen Locatie" : $"{s.Location.Zone}-{s.Location.Rack}-{s.Location.Shelf}-{s.Location.Box}"))
                .ForMember(d => d.PartName, opt => opt.MapFrom(s => s.Part.Name));

        }

    }
}
