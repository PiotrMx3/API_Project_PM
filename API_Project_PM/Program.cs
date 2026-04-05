
using API_Project_PM.Core.Categories;
using API_Project_PM.Core.Database;
using API_Project_PM.Core.Models;
using API_Project_PM.Core.Services;
using API_Project_PM.Core.Services.Categories;
using API_Project_PM.Core.Services.Locations;
using API_Project_PM.Core.Services.Parts;
using API_Project_PM.Core.Services.Suppliers;
using Microsoft.EntityFrameworkCore;

namespace API_Project_PM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDBContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")))
            );

            builder.Services.AddScoped<ICategoryRepository, CategoryService>();

            builder.Services.AddRouting(options => options.LowercaseUrls = true);
            builder.Services.AddControllers();
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


            app.MapControllers();

            app.Run();
        }
    }
}
