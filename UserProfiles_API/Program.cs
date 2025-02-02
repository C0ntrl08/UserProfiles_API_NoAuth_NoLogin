using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MySql.EntityFrameworkCore;
using System.Configuration;
using UserProfiles_API.Databases;

namespace UserProfiles_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<UserProfilesDbContext>(options =>
            options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")!));


            using (var connection = new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connection to the Database successful!");
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Could not connect to the database!", ex);
                }
            }

            // Add Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllers();


            var app = builder.Build();

            // Swagger UI -> implemented in the launchSettings.json aswell
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();


            app.Run();
        }
    }
}
