using API_Project_PM.Core.Categories;
using API_Project_PM.Core.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API_Minimal_Project_PM.Eindpoints
{
    public static class CategoryEndpoints
    {
        public static void MapCategoryEndpoints(this IEndpointRouteBuilder app)
        {
            var categoryGroup = app.MapGroup("/api/Category").WithTags("Category");

            // 1. GET: GetAllCategories
            categoryGroup.MapGet("/", async Task<Results<NotFound, Ok<IEnumerable<Category>>>> (ICategoryRepository repo) =>
            {
                var result = await repo.GetAllCategories();
                if (!result.Any()) return TypedResults.NotFound();

                return TypedResults.Ok(result);
            })
            .Produces(StatusCodes.Status500InternalServerError);

            // 2. GET: GetCategoryById
            categoryGroup.MapGet("/{id:int}", async Task<Results<NotFound, Ok<Category>>> (int id, ICategoryRepository repo) =>
            {
                var result = await repo.GetCategoryById(id);
                if (result is null) return TypedResults.NotFound();

                return TypedResults.Ok(result);
            })
            .WithName("GetCategoryById")
            .Produces(StatusCodes.Status500InternalServerError);

            // 3. POST: CreateCategory
            categoryGroup.MapPost("/", async Task<Results<BadRequest, CreatedAtRoute<Category>>> (Category item, ICategoryRepository repo) =>
            {
                if (item is null) return TypedResults.BadRequest();

                await repo.CreateCategory(item);

                return TypedResults.CreatedAtRoute(item, "GetCategoryById", new { id = item.Id });
            })
            .Produces(StatusCodes.Status500InternalServerError);

            // 4. PUT: UpdateCategory
            categoryGroup.MapPut("/{id:int}", async Task<Results<BadRequest, NotFound, NoContent>> (int id, Category item, ICategoryRepository repo) =>
            {
                if (item is null || id != item.Id) return TypedResults.BadRequest();

                bool updated = await repo.UpdateCategory(id, item);
                if (!updated) return TypedResults.NotFound();

                return TypedResults.NoContent();
            })
            .Produces(StatusCodes.Status500InternalServerError);

            // 5. DELETE: DeleteCategory
            categoryGroup.MapDelete("/{id:int}", async Task<Results<NotFound, NoContent>> (int id, ICategoryRepository repo) =>
            {
                bool deleted = await repo.DeleteCategory(id);
                if (!deleted) return TypedResults.NotFound();

                return TypedResults.NoContent();
            })
            .Produces(StatusCodes.Status500InternalServerError);
        }

    }
}
