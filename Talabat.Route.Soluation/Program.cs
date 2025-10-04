

using Microsoft.EntityFrameworkCore;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Data.DataSeed;

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

            WebApplicationbuilder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));



            var app = WebApplicationbuilder.Build();
            #endregion


            #region configre

            #region Update-Database

            using var Scope = app.Services.CreateScope();
            var services = Scope.ServiceProvider;

            var _DbContext = services.GetRequiredService<StoreContext>();

            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _DbContext.Database.MigrateAsync();//update-database
                await StoreContextSeed.SeedAsync(_DbContext);
            }
            catch (Exception ex)
            {

                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has happened while migrating");


            } 
            #endregion
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
