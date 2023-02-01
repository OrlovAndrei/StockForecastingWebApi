using StockForecastingWebApi.Services;
using System.Reflection;

namespace StockForecastingWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var AllowSpecificOrigins = "_AllowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: AllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:3000");
                                  });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<IDataFetcher, YahooDataFetcher>();
            builder.Services.AddTransient<IForecasterProvider, ForecasterProvider>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(AllowSpecificOrigins);

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}

