using Microsoft.EntityFrameworkCore;
using Práctica_Controller_Service_Repository.Data;
using Práctica_Controller_Service_Repository.Repositories.Implementations;
using Práctica_Controller_Service_Repository.Repositories.Interfaces;
using Práctica_Controller_Service_Repository.Services.Implementations;
using Práctica_Controller_Service_Repository.Services.Interfaces;
namespace Práctica_Controller_Service_Repository
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            var dbPath = Path.Combine(builder.Environment.ContentRootPath, "ABMProductos.db");
            builder.Services.AddDbContext<ProductContext>(dbContextOptions =>
                dbContextOptions.UseSqlite($"Data Source={dbPath}"));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ProductContext>();
                db.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
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