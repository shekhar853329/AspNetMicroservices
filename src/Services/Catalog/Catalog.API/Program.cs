using Catalog.API.Data;
using Catalog.API.Repositories;

namespace Catalog.API
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddScoped<ICatalogContext, CatalogContext>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            // Add services to the container.
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
            app.UseRouting();

            app.UseEndpoints(endPoint => endPoint.MapControllers());

            app.Run();
        }
    }

    internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
    {
        //sample CHange
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}