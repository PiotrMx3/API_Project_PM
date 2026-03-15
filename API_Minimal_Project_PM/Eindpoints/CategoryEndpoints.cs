using API_Minimal_Project_PM.Models;
using API_Minimal_Project_PM.Services.Categories;

namespace API_Minimal_Project_PM.Eindpoints
{
    public static class CategoryEndpoints
    {
        public static void MapCategoryEndpoints(this IEndpointRouteBuilder app)
        {
            var categoryGroup = app.MapGroup("/api/Category").WithTags("Category");

            // 1. GET: GetAllCategories
            categoryGroup.MapGet("/", async (ICategoryRepository repo) =>
            {
                var result = await repo.GetAllCategories();
                if (!result.Any()) return Results.NotFound();

                return Results.Ok(result);
            })
            .Produces<IEnumerable<Category>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

            // 2. GET: GetCategoryById
            categoryGroup.MapGet("/{id:int}", async (int id, ICategoryRepository repo) =>
            {
                var result = await repo.GetCategoryById(id);
                if (result is null) return Results.NotFound();

                return Results.Ok(result);
            })
            .WithName("GetCategoryById")
            .Produces<Category>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

            // 3. POST: CreateCategory
            categoryGroup.MapPost("/", async (Category item, ICategoryRepository repo) =>
            {
                if (item is null) return Results.BadRequest();

                await repo.CreateCategory(item);

                return Results.CreatedAtRoute("GetCategoryById", new { id = item.Id }, item);
            })
            .Produces<Category>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

            // 4. PUT: UpdateCategory
            categoryGroup.MapPut("/{id:int}", async (int id, Category item, ICategoryRepository repo) =>
            {
                if (item is null || id != item.Id) return Results.BadRequest();

                bool updated = await repo.UpdateCategory(id, item);
                if (!updated) return Results.NotFound();

                return Results.NoContent();
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

            // 5. DELETE: DeleteCategory
            categoryGroup.MapDelete("/{id:int}", async (int id, ICategoryRepository repo) =>
            {
                bool deleted = await repo.DeleteCategory(id);
                if (!deleted) return Results.NotFound();

                return Results.NoContent();
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        }

    }
}
