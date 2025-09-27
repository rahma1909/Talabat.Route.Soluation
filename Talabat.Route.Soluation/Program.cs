

using Microsoft.EntityFrameworkCore;
using Talabat.Repository.Data;

namespace Talabat.Route.Soluation
 
{
    public class Program
    {
        public static async Task  Main(string[] args)
        {
            #region createhostbuilder ==>configreServices
            var WebApplicationbuilder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            WebApplicationbuilder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            WebApplicationbuilder.Services.AddEndpointsApiExplorer();
            WebApplicationbuilder.Services.AddSwaggerGen();

            WebApplicationbuilder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(WebApplicationbuilder.Configuration.GetConnectionString("DefaultConnection"));
            });
          
               


            var app = WebApplicationbuilder.Build();
            #endregion


            #region configre


            using var Scope = app.Services.CreateScope();
            var services = Scope.ServiceProvider;

            var _DbContext = services.GetRequiredService<StoreContext>();

            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _DbContext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {

                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has happened while migrating");


            }
            // Config  throw;ure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

         


            app.MapControllers();

            app.Run();
        } 
        #endregion
    }
}
