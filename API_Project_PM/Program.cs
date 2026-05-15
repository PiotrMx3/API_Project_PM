using API_Project_PM.Core.Database;
using API_Project_PM.Core.Database.Seed;
using API_Project_PM.Core.Services.Categories;
using API_Project_PM.Core.Services.Locations;
using API_Project_PM.Core.Services.Parts;
using API_Project_PM.Core.Services.PartsSuppliers;
using API_Project_PM.Core.Services.StockItems;
using API_Project_PM.Core.Services.StockMovements;
using API_Project_PM.Core.Services.Suppliers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace API_Project_PM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            if (builder.Environment.IsStaging())
            {
                builder.Services.AddDbContext<AppDBContext>(options =>
                options.UseInMemoryDatabase("InMemoryDb"));
            }
            else
            {
                builder.Services.AddDbContext<AppDBContext>(options =>
                    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")))
                );
            }

            var autoMapperKey = builder.Configuration["AutoMapperSettings:LicenseKey"];

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.LicenseKey = autoMapperKey;
            }, AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddScoped<ICategoryRepository, CategoryService>();
            builder.Services.AddScoped<ILocationRepository, LocationService>();
            builder.Services.AddScoped<ISupplierRepository, SupplierService>();
            builder.Services.AddScoped<IPartRepository, PartService>();
            builder.Services.AddScoped<IPartSupplierRepository, PartSupplierService>();
            builder.Services.AddScoped<IStockItemRepository, StockItemService>();
            builder.Services.AddScoped<IStockMovementRepository, StockMovementService>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!))
                    };
                });

            builder.Services.AddRouting(options => options.LowercaseUrls = true);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Voer je JWT token in"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
            {
                // Manually scoped service 
                using (var scope = app.Services.CreateScope())
                {
                    AppDBContext context = scope.ServiceProvider.GetRequiredService<AppDBContext>();
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                    DatabaseSeeder.Seed(context);
                }

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
