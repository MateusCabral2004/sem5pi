using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Sempi5.Domain.TodoItem;

namespace Sempi5.Infrastructure.Databases;

public class SQLDatabase : IDatabase
{

    public void connectDB(WebApplicationBuilder builder)
    {

        var connectionString = "Server=" + builder.Configuration["DataBase:Server"] +
                               ";Port=" + builder.Configuration["DataBase:Port"] +
                               ";Database=" + builder.Configuration["DataBase:Name"] +
                               ";Uid=" + builder.Configuration["DataBase:User"] +
                               ";Pwd=" + builder.Configuration["DataBase:Password"] + ";";

        Console.WriteLine(connectionString);

        builder.Services.AddDbContext<DBContext>(opt =>
            opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        new MySqlConnection(connectionString).Open();

    }


}