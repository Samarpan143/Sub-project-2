using DataServiceLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebServiceLayer.CustomMiddleware;

namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IDataService>(svc =>
                new DataService("Host=localhost;Database=imdb;Username=postgres;Password=Family@1"));

            builder.Services.AddRouting();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection(); 
            //app.UseAuth();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }


}
