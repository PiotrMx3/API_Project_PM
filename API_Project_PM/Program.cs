
using API_Project_PM.Services.Locations;
using API_Project_PM.Services.Parts;
using API_Project_PM.Services.Suppliers;

namespace API_Project_PM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddScoped<ILocationsRepository, InMemoryLocationsRepository>();
            builder.Services.AddScoped<IPartsRepository, InMemoryPartsRepository>();
            builder.Services.AddScoped<ISuppliersRepository, InMemorySuppliersRepository>();


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
