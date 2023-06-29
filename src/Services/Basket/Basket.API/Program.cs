using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Discount.Grpc.Protos;

namespace Basket.API
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllers();
            builder.Services.AddStackExchangeRedisCache(action =>
            {
                action.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionSettings");
            });

            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o => o.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]));
            builder.Services.AddScoped<DiscountGrpcService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();

            app.UseEndpoints(p => p.MapControllers());

            app.Run();
        }
    }
}
