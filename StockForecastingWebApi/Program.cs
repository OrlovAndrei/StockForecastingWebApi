using StockForecastingWebApi.Services;
using System.Reflection;

namespace StockForecastingWebApi
{
    public class Program
    {
        public static Dictionary<string, IForecaster> Forecasters { get; set; }

        public static void Main(string[] args)
        {
            Forecasters = GetForecasters();

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

        public static Dictionary<string, IForecaster> GetForecasters()
        {
            return Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(a => a.GetConstructor(Type.EmptyTypes) != null)
                .Select(Activator.CreateInstance)
                .OfType<IForecaster>()
                .ToDictionary(x => x
                    .GetType()
                    .GetCustomAttribute<ForecasterAttribute>()
                    .Name);
        }
    }
}

