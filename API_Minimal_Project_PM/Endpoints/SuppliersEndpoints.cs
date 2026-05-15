using API_Project_PM.Core.CustomException;
using API_Project_PM.Core.CustomExceptions;
using API_Project_PM.Core.DTOs.Suppliers;
using API_Project_PM.Core.Models;
using API_Project_PM.Core.Services.Suppliers;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API_Minimal_Project_PM.Endpoints
{
    public static class SuppliersEndpoints
    {
        public static void MapSuppliersEndpoints(this IEndpointRouteBuilder app)
        {
            var suppliersGroup = app.MapGroup("/api/suppliers").WithTags("Suppliers").RequireAuthorization();

            suppliersGroup.MapGet("/",
                async Task<Ok<IEnumerable<SupplierDto>>> (
                    ISupplierRepository repo,
                    IMapper mapper) =>
                {
                    var suppliers = await repo.GetAllAsync();
                    return TypedResults.Ok(mapper.Map<IEnumerable<SupplierDto>>(suppliers));
                });

            suppliersGroup.MapGet("/{id:int}",
                async Task<Results<Ok<SupplierWithPartsDto>, NotFound, BadRequest>> (
                    int id,
                    ISupplierRepository repo,
                    IMapper mapper) =>
                {
                    if (id <= 0) return TypedResults.BadRequest();

                    var supplier = await repo.GetByIdAsync(id);
                    if (supplier is null) return TypedResults.NotFound();

                    return TypedResults.Ok(mapper.Map<SupplierWithPartsDto>(supplier));
                })
            .WithName("GetSupplierById");

            suppliersGroup.MapPost("/",
                async Task<Results<CreatedAtRoute<SupplierDto>, BadRequest, Conflict<object>>> (
                    CreateSupplierDto dto,
                    ISupplierRepository repo,
                    IMapper mapper) =>
                {
                    if (dto is null) return TypedResults.BadRequest();

                    var entity = mapper.Map<Supplier>(dto);

                    try
                    {
                        var created = await repo.CreateAsync(entity);
                        var result = mapper.Map<SupplierDto>(created);
                        return TypedResults.CreatedAtRoute(result, "GetSupplierById", new { id = result.Id });
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

            suppliersGroup.MapPut("/{id:int}",
                async Task<Results<NoContent, NotFound, BadRequest, Conflict<object>>> (
                    int id,
                    UpdateSupplierDto dto,
                    ISupplierRepository repo,
                    IMapper mapper) =>
                {
                    if (id <= 0) return TypedResults.BadRequest();
                    if (dto is null) return TypedResults.BadRequest();

                    var entity = mapper.Map<Supplier>(dto);
                    entity.Id = id;

                    try
                    {
                        bool updated = await repo.UpdateAsync(entity);
                        if (!updated) return TypedResults.NotFound();
                        return TypedResults.NoContent();
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

            suppliersGroup.MapDelete("/{id:int}",
                async Task<Results<NoContent, NotFound, BadRequest, Conflict<object>>> (
                    int id,
                    ISupplierRepository repo) =>
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
