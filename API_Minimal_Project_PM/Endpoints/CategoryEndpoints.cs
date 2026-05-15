using API_Project_PM.Core.CustomException;
using API_Project_PM.Core.CustomExceptions;
using API_Project_PM.Core.DTOs.Categories;
using API_Project_PM.Core.Models;
using API_Project_PM.Core.Services.Categories;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API_Minimal_Project_PM.Eindpoints
{
    public static class CategoryEndpoints
    {
        public static void MapCategoryEndpoints(this IEndpointRouteBuilder app)
        {
            var categoryGroup = app.MapGroup("/api/categories").WithTags("Categories").RequireAuthorization();

            categoryGroup.MapGet("/",
                async Task<Ok<IEnumerable<CategoryDto>>> (
                    ICategoryRepository repo,
                    IMapper mapper) =>
                {
                    var categories = await repo.GetAllAsync();
                    return TypedResults.Ok(mapper.Map<IEnumerable<CategoryDto>>(categories));
                });

            categoryGroup.MapGet("/{id:int}",
                async Task<Results<Ok<CategoryDto>, NotFound, BadRequest>> (
                int id,
                ICategoryRepository repo,
                IMapper mapper) =>
                {
                    if (id <= 0) return TypedResults.BadRequest();

                    var category = await repo.GetByIdAsync(id);
                    if (category is null) return TypedResults.NotFound();

                    return TypedResults.Ok(mapper.Map<CategoryDto>(category));
                })
            .WithName("GetCategoryById");

            categoryGroup.MapPost("/",
                async Task<Results<CreatedAtRoute<CategoryDto>, BadRequest, Conflict<object>>> (
                CreateCategoryDto dto,
                ICategoryRepository repo,
                IMapper mapper) =>
                {
                    if (dto is null) return TypedResults.BadRequest();

                    var category = mapper.Map<Category>(dto);

                    try
                    {
                        var created = await repo.CreateAsync(category);
                        var result = mapper.Map<CategoryDto>(created);
                        return TypedResults.CreatedAtRoute(result, "GetCategoryById", new { id = result.Id });
                    }
                    catch (ConflictException e)
                    {
                        return TypedResults.Conflict<object>(new { conflict = e.Message});
                    }
                    catch (DbUpdateException)
                    {
                        throw;
                    }
                });

            categoryGroup.MapPut("/{id:int}",
                async Task<Results<NoContent, NotFound, BadRequest, Conflict>> (
                int id,
                UpdateCategoryDto dto,
                ICategoryRepository repo,
                IMapper mapper) =>
                {
                    if (id <= 0) return TypedResults.BadRequest();
                    if (dto is null) return TypedResults.BadRequest();

                    var category = mapper.Map<Category>(dto);
                    category.Id = id;

                    try
                    {
                        bool updated = await repo.UpdateAsync(category);
                        if (!updated) return TypedResults.NotFound();
                        return TypedResults.NoContent();
                    }
                    catch (ConflictException)
                    {
                        return TypedResults.Conflict();
                    }
                    catch (DbUpdateException)
                    {
                        throw;
                    }
                });

            categoryGroup.MapDelete("/{id:int}",
                async Task<Results<NoContent, NotFound, BadRequest, Conflict<object>>> (
                int id,
                ICategoryRepository repo) =>
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
