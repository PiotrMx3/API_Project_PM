using API_Project_PM.Core.CustomException;
using API_Project_PM.Core.DTOs.StockItems;
using API_Project_PM.Core.Services.StockItems;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API_Minimal_Project_PM.Eindpoints
{
    public static class StockItemsEndpoints
    {
        public static void MapStockItemsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/stock-items").WithTags("StockItems").RequireAuthorization();

            group.MapGet("/",
                async Task<Ok<IEnumerable<StockItemDto>>> (
                    IStockItemRepository repo,
                    IMapper mapper) =>
                {
                    var items = await repo.GetAllAsync();
                    return TypedResults.Ok(mapper.Map<IEnumerable<StockItemDto>>(items));
                });

            group.MapGet("/location/{id:int}",
                async Task<Results<Ok<IEnumerable<StockItemDto>>, BadRequest>> (
                    int id,
                    IStockItemRepository repo,
                    IMapper mapper) =>
                {
                    if (id <= 0) return TypedResults.BadRequest();

                    var items = await repo.GetByLocationIdAsync(id);
                    return TypedResults.Ok(mapper.Map<IEnumerable<StockItemDto>>(items));
                });

            group.MapGet("/part/{id:int}",
                async Task<Results<Ok<IEnumerable<StockItemDto>>, BadRequest>> (
                    int id,
                    IStockItemRepository repo,
                    IMapper mapper) =>
                {
                    if (id <= 0) return TypedResults.BadRequest();

                    var items = await repo.GetByPartIdAsync(id);
                    return TypedResults.Ok(mapper.Map<IEnumerable<StockItemDto>>(items));
                });

            group.MapDelete("/part/{partId:int}/location/{locationId:int}",
                async Task<Results<NoContent, NotFound, BadRequest, Conflict<object>>> (
                    int partId,
                    int locationId,
                    IStockItemRepository repo) =>
                {
                    if (partId <= 0 || locationId <= 0) return TypedResults.BadRequest();

                    try
                    {
                        bool deleted = await repo.DeleteAsync(partId, locationId);
                        if (!deleted) return TypedResults.NotFound();
                        return TypedResults.NoContent();
                    }
                    catch (ConflictException e)
                    {
                        return TypedResults.Conflict<object>(new { conflict = e.Message });
                    }
                });
        }
    }
}
