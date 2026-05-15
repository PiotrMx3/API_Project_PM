using API_Project_PM.Core.CustomException;
using API_Project_PM.Core.DTOs.StockMovements;
using API_Project_PM.Core.Enums;
using API_Project_PM.Core.Models;
using API_Project_PM.Core.Services.StockItems;
using API_Project_PM.Core.Services.StockMovements;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API_Minimal_Project_PM.Endpoints
{
    public static class StockMovementEndpoints
    {
        public static void MapStockMovementEndpoints(this IEndpointRouteBuilder app)
        {
            var stockMovementsGroup = app.MapGroup("/api/stock-movements").WithTags("StockMovements").RequireAuthorization();

            stockMovementsGroup.MapGet("/",
                async Task<Ok<IEnumerable<StockMovementDto>>> (
                    IStockMovementRepository repo,
                    IMapper mapper) =>
                {
                    var movements = await repo.GetAllStockMovements();
                    return TypedResults.Ok(mapper.Map<IEnumerable<StockMovementDto>>(movements));
                });

            stockMovementsGroup.MapGet("/{id:int}",
                async Task<Results<Ok<StockMovementDto>, NotFound, BadRequest>> (
                    int id,
                    IStockMovementRepository repo,
                    IMapper mapper) =>
                {
                    if (id <= 0) return TypedResults.BadRequest();

                    var movement = await repo.GetStockMovementById(id);
                    if (movement is null) return TypedResults.NotFound();

                    return TypedResults.Ok(mapper.Map<StockMovementDto>(movement));
                });

            stockMovementsGroup.MapPut("/movetype/{typeMove:int}/part/{partId:int}/location/{locationId:int}/quantity/{quantity:int}",
                async Task<Results<NoContent, BadRequest, NotFound<object>, Conflict<object>>> (
                    int typeMove,
                    int partId,
                    int locationId,
                    int quantity,
                    IStockItemRepository stockItemRepo,
                    IStockMovementRepository movementRepo) =>
                {
                    if (partId <= 0 || locationId <= 0) return TypedResults.BadRequest();

                    try
                    {
                        bool result = await stockItemRepo.UpsertAsync(partId, locationId, quantity, (MovementType)typeMove);
                        if (!result) return TypedResults.NotFound<object>(new { notFound = "Onderdeel of locatie niet gevonden" });

                        await movementRepo.CreateStockMovement(new StockMovement
                        {
                            PartId = partId,
                            LocationId = locationId,
                            Quantity = quantity,
                            MovementType = (MovementType)typeMove,
                            TransferGroupId = Guid.NewGuid()
                        });

                        return TypedResults.NoContent();
                    }
                    catch (ConflictException e)
                    {
                        return TypedResults.Conflict<object>(new { conflict = e.Message });
                    }
                    catch (NotFoundException e)
                    {
                        return TypedResults.NotFound<object>(new { notFound = e.Message });
                    }
                    catch (DbUpdateException)
                    {
                        throw;
                    }
                });

            stockMovementsGroup.MapPut("/{id:int}", (int id) =>
                Results.StatusCode(405));

            stockMovementsGroup.MapDelete("/{id:int}", (int id) =>
                Results.StatusCode(405));
        }
    }
}
