using API_Project_PM.Core.CustomException;
using API_Project_PM.Core.CustomExceptions;
using API_Project_PM.Core.DTOs.Locations;
using API_Project_PM.Core.Models;
using API_Project_PM.Core.Services.Locations;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API_Minimal_Project_PM.Eindpoints
{
    public static class LocationEndpoints
    {
        public static void MapLocationsEndpoints(this IEndpointRouteBuilder app)
        {
            var locationsGroup = app.MapGroup("/api/locations").WithTags("Locations").RequireAuthorization();

            locationsGroup.MapGet("/",
                async Task<Ok<IEnumerable<LocationDto>>> (
                    ILocationRepository repo,
                    IMapper mapper) =>
                {
                    var locations = await repo.GetAllAsync();
                    return TypedResults.Ok(mapper.Map<IEnumerable<LocationDto>>(locations));
                });

            locationsGroup.MapGet("/{id:int}",
                async Task<Results<Ok<LocationDto>, NotFound, BadRequest>> (
                int id,
                ILocationRepository repo,
                IMapper mapper) =>
                {
                    if (id <= 0) return TypedResults.BadRequest();

                    var location = await repo.GetByIdAsync(id);
                    if (location is null) return TypedResults.NotFound();

                    return TypedResults.Ok(mapper.Map<LocationDto>(location));
                })
            .WithName("GetLocationById");

            locationsGroup.MapPost("/",
                async Task<Results<CreatedAtRoute<LocationDto>, BadRequest, Conflict<object>>> (
                CreateLocationDto dto,
                ILocationRepository repo,
                IMapper mapper) =>
                {
                    if (dto is null) return TypedResults.BadRequest();

                    var entity = mapper.Map<Location>(dto);

                    try
                    {
                        var created = await repo.CreateAsync(entity);
                        var result = mapper.Map<LocationDto>(created);
                        return TypedResults.CreatedAtRoute(result, "GetLocationById", new { id = result.Id });
                    }
                    catch (ConflictException e)
                    {
                        return TypedResults.Conflict<object>(new { conflict = e.Message });
                    }
                    catch (DbUpdateException)
                    {
                        throw;
                    }
                });

            locationsGroup.MapPut("/{id:int}",
                async Task<Results<NoContent, NotFound, BadRequest, Conflict<object>>> (
                int id,
                UpdateLocationDto dto,
                ILocationRepository repo,
                IMapper mapper) =>
                {
                    if (id <= 0) return TypedResults.BadRequest();
                    if (dto is null) return TypedResults.BadRequest();

                    var entity = mapper.Map<Location>(dto);
                    entity.Id = id;

                    try
                    {
                        bool updated = await repo.UpdateAsync(entity);
                        if (!updated) return TypedResults.NotFound();
                        return TypedResults.NoContent();
                    }
                    catch (ConflictException e)
                    {
                        return TypedResults.Conflict<object>(new {conflict = e.Message });
                    }
                    catch (DbUpdateException)
                    {
                        throw;
                    }
                });

            locationsGroup.MapDelete("/{id:int}",
                async Task<Results<NoContent, NotFound, BadRequest, Conflict<object>>> (
                int id,
                ILocationRepository repo) =>
                {
                    if (id <= 0) return TypedResults.BadRequest();

                    try
                    {
                        bool deleted = await repo.DeleteAsync(id);
                        if (!deleted) return TypedResults.NotFound();
                        return TypedResults.NoContent();
                    }
                    catch (CannotDeleteException e)
                    {
                        return TypedResults.Conflict<object>(new { conflict = e.Message });
                    }
                    catch (DbUpdateException)
                    {
                        throw;
                    }
                });
        }
    }
}
