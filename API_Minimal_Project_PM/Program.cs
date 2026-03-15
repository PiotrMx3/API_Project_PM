using API_Minimal_Project_PM.Services.Locations;
using API_Minimal_Project_PM.Services.Categories;
using API_Minimal_Project_PM.Services.Parts;
using API_Minimal_Project_PM.Services.Suppliers;
using API_Minimal_Project_PM.Services.StockMovements;
using API_Minimal_Project_PM.Models;
using API_Minimal_Project_PM.Eindpoints;

namespace API_Minimal_Project_PM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();


            builder.Services.AddScoped<ILocationsRepository, InMemoryLocationsRepository>();
            builder.Services.AddScoped<IPartsRepository, InMemoryPartsRepository>();
            builder.Services.AddScoped<ISuppliersRepository, InMemorySuppliersRepository>();
            builder.Services.AddScoped<ICategoryRepository, InMemeoryCategoryRepository>();
            builder.Services.AddScoped<IStockMovementRepository, InMemoryStockMovementRepository>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapCategoryEndpoints();

            app.Run();
        }
    }
}
