using API_Project_PM.Core.DTOs.PartsSuppliers;
using API_Project_PM.Core.Models;
using API_Project_PM.Core.Services.PartsSuppliers;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API_Minimal_Project_PM.Eindpoints
{
    public static class PartSupplierEndpoints
    {
        public static void MapPartSupplierEndpoints(this IEndpointRouteBuilder app)
        {
            var partSupplierGroup = app.MapGroup("/api/part-suppliers").WithTags("PartSuppliers").RequireAuthorization();

            partSupplierGroup.MapGet("/",
                async Task<Ok<IEnumerable<PartSupplierDto>>> (
                    IPartSupplierRepository repo,
                    IMapper mapper) =>
                {
                    var items = await repo.GetAllAsync();
                    return TypedResults.Ok(mapper.Map<IEnumerable<PartSupplierDto>>(items));
                });

            partSupplierGroup.MapGet("/part/{partId:int}/supplier/{supplierId:int}",
                async Task<Results<Ok<PartSupplierDto>, NotFound, BadRequest>> (
                int partId,
                int supplierId,
                IPartSupplierRepository repo,
                IMapper mapper) =>
                {
                    if (partId <= 0 || supplierId <= 0) return TypedResults.BadRequest();

                    var item = await repo.GetById(partId, supplierId);
                    if (item is null) return TypedResults.NotFound();

                    return TypedResults.Ok(mapper.Map<PartSupplierDto>(item));
                })
            .WithName("GetPartSupplierById");

            partSupplierGroup.MapPost("/",
                async Task<Results<CreatedAtRoute<PartSupplierDto>, BadRequest, Conflict<object>>> (
                CreatePartSupplierDto dto,
                IPartSupplierRepository repo,
                IMapper mapper) =>
                {
                    if (dto.PartId <= 0 || dto.SupplierId <= 0) return TypedResults.BadRequest();

                    var entity = mapper.Map<PartSupplier>(dto);

                    try
                    {
                        var created = await repo.CreateAsync(entity);
                        var result = mapper.Map<PartSupplierDto>(created);
                        return TypedResults.CreatedAtRoute(result, "GetPartSupplierById", new { partId = created.PartId, supplierId = created.SupplierId });
                    }
                    catch (DbUpdateException)
                    {
                        return TypedResults.Conflict<object>(new { conflict = "ID combinatie is niet correct" });
                    }
                });

            partSupplierGroup.MapPut("/part/{partId:int}/supplier/{supplierId:int}",
                async Task<Results<NoContent, NotFound, BadRequest, Conflict<object>>> (
                int partId,
                int supplierId,
                UpdatePartSupplierDto dto,
                IPartSupplierRepository repo,
                IMapper mapper) =>
                {
                    if (partId <= 0 || supplierId <= 0) return TypedResults.BadRequest();

                    var entity = mapper.Map<PartSupplier>(dto);
                    entity.PartId = partId;
                    entity.SupplierId = supplierId;

                    try
                    {
                        bool updated = await repo.UpdetAsync(entity);
                        if (!updated) return TypedResults.NotFound();
                        return TypedResults.NoContent();
                    }
                    catch (DbUpdateException)
                    {
                        return TypedResults.Conflict<object>(new { conflict = "ID combinatie is niet correct" });
                    }
                });

            partSupplierGroup.MapDelete("/part/{partId:int}/supplier/{supplierId:int}",
                async Task<Results<NoContent, NotFound, BadRequest>> (
                int partId,
                int supplierId,
                IPartSupplierRepository repo) =>
                {
                    if (partId <= 0 || supplierId <= 0) return TypedResults.BadRequest();

                    bool deleted = await repo.DeleteAsync(partId, supplierId);
                    if (!deleted) return TypedResults.NotFound();
                    return TypedResults.NoContent();
                });
        }
    }
}
