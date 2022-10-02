using HangmanAPI.Data.Context;
using HangmanAPI.Data.DataInit;
using HangmanAPI.Repository.Interface;
using HangmanAPI.Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace HangmanAPI.Utility {
    public static class ServiceExtensions {

        public static void ConfigureScopedServices(ref WebApplicationBuilder builder) {
            //scoped
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            //transient
            builder.Services.AddTransient<DataInitializer>();
        }

        public static void ConfigureDbContext(ref WebApplicationBuilder builder, ConfigurationManager configManager ) {
            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configManager.GetConnectionString("Default")));
        }
    }
}
