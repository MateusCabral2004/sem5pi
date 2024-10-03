using System.Text.Json;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Sempi5.Domain.Shared;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.Shared;
using Sempi5.Infrastructure.TodoItemRepository;
using IDatabase = Sempi5.Infrastructure.Databases.IDatabase;

namespace Sempi5
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            CreateDataBase(builder);

            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();

            app.MapControllers();

            app.Run();
        }

        // See what database is
        public static void CreateDataBase(WebApplicationBuilder builder)
        {

            string name = "Sempi5.Infrastructure.Databases." + builder.Configuration["DataBase:Type"];

            Type dbType = null;
            try
            {
                dbType = Type.GetType(name);
                _ = (IDatabase)Activator.CreateInstance(dbType);
            }
            catch (Exception)
            {
                // Exit application
                Console.WriteLine("Database Type Invalid. Please check the configuration file!\nApplication will exit");
                Environment.Exit(2);
            }

            try
            {
                ((IDatabase)Activator.CreateInstance(dbType)).connectDB(builder);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
                Console.WriteLine("Database not found\nApplication will exit");
                Environment.Exit(3);
                
            }

        Console.WriteLine("Database Connected");

        }

        public void ConfigureMyServices(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ITodoItemRepository, TodoItemRepository>();
            //services.AddTransient<CategoryService>();
        }
    }
}