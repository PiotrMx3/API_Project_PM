using API_Project_PM.Core.CustomException;
using API_Project_PM.Core.CustomExceptions;
using API_Project_PM.Core.DTOs.Parts;
using API_Project_PM.Core.Models;
using API_Project_PM.Core.Services.Parts;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API_Minimal_Project_PM.Eindpoints
{
    public static class PartsEndpoints
    {
        public static void MapPartsEndpoints(this IEndpointRouteBuilder app)
        {
            var partsGroup = app.MapGroup("/api/parts").WithTags("Parts").RequireAuthorization();

            partsGroup.MapGet("/",
                async Task<Ok<IEnumerable<PartDto>>> (
                    IPartRepository repo,
                    IMapper mapper) =>
                {
                    var parts = await repo.GetAllAsync();
                    return TypedResults.Ok(mapper.Map<IEnumerable<PartDto>>(parts));
                });

            partsGroup.MapGet("/{id:int}",
                async Task<Results<Ok<PartWithSuppliersDto>, NotFound, BadRequest>> (
                int id,
                IPartRepository repo,
                IMapper mapper) =>
                {
                    if (id <= 0) return TypedResults.BadRequest();

                    var part = await repo.GetByIdAsync(id);
                    if (part is null) return TypedResults.NotFound();

                    return TypedResults.Ok(mapper.Map<PartWithSuppliersDto>(part));
                })
            .WithName("GetPartById");

            partsGroup.MapPost("/",
                async Task<Results<CreatedAtRoute<PartDto>, BadRequest, Conflict<object>, NotFound<object>>> (
                CreatePartDto dto,
                IPartRepository repo,
                IMapper mapper) =>
                {
                    if (dto is null) return TypedResults.BadRequest();

                    var entity = mapper.Map<Part>(dto);

                    try
                    {
                        var created = await repo.CreateAsync(entity);
                        var result = mapper.Map<PartDto>(created);
                        return TypedResults.CreatedAtRoute(result, "GetPartById", new { id = result.Id });
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

            partsGroup.MapPut("/{id:int}",
                async Task<Results<NoContent, NotFound<object>, BadRequest>> (
                int id,
                UpdatePartDto dto,
                IPartRepository repo,
                IMapper mapper) =>
                {
                    if (id <= 0) return TypedResults.BadRequest();
                    if (dto is null) return TypedResults.BadRequest();

                    var entity = mapper.Map<Part>(dto);
                    entity.Id = id;

                    try
                    {
                        bool updated = await repo.UpdateAsync(entity);
                        if (!updated) return TypedResults.NotFound<object>(null);
                        return TypedResults.NoContent();
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

            partsGroup.MapDelete("/{id:int}",
                async Task<Results<NoContent, NotFound, BadRequest, Conflict<object>>> (
                int id,
                IPartRepository repo) =>
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
                });
        }
    }
}
