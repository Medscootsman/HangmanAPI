using HangmanAPI.Data.Context;
using HangmanAPI.Data.DataInit;
using HangmanAPI.Model.Profile;
using HangmanAPI.Repository.Interface;
using HangmanAPI.Repository.Repository;
using HangmanAPI.Service.Interface;
using HangmanAPI.Service.Service;
using Microsoft.EntityFrameworkCore;

namespace HangmanAPI.Utility {
    public static class ServiceExtensions {

        public static void ConfigureScopedServices(ref WebApplicationBuilder builder) {
            //scoped
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(GameProfile), typeof(GuessProfile), typeof(WordProfile));
            builder.Services.AddScoped<IGameService, GameService>();
            builder.Services.AddScoped<IWordService, WordService>();
            builder.Services.AddScoped<IGuessService, GuessService>();
            //transient
            builder.Services.AddTransient<DataInitializer>();

        }

        public static void ConfigureDbContext(ref WebApplicationBuilder builder, ConfigurationManager configManager ) {
            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configManager.GetConnectionString("Default")));
        }
    }
}
