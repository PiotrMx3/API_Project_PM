using API_Project_PM.Core.Models;
using AutoMapper;

namespace API_Project_PM.Core.DTOs.StockMovements
{
    public class StockMovementProfile : Profile
    {
        public StockMovementProfile()
        {
            CreateMap<StockMovement, StockMovementDto>()
                .ForMember(s => s.Part, opt => opt.MapFrom(d => d.Part.Name))
                .ForMember(s => s.Location, opt => opt.MapFrom(s => s.Location == null ? "Geen Locatie" : $"{s.Location.Zone}-{s.Location.Rack}-{s.Location.Shelf}-{s.Location.Box}"))
                .ForMember(s => s.MovementType, opt => opt.MapFrom(s => s.MovementType == Enums.MovementType.In ? "IN" : "OUT"));
        }
    }
}
