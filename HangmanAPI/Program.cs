using HangmanAPI.Data.Context;
using HangmanAPI.Repository.Repository;
using HangmanAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using HangmanAPI.Data.DataInit;
using HangmanAPI;
using Microsoft.Extensions.DependencyInjection;
using HangmanAPI.Utility;

var builder = WebApplication.CreateBuilder(args);

ServiceExtensions.ConfigureScopedServices(ref builder);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope()) {
    var context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
    var dataInit = serviceScope.ServiceProvider.GetService<DataInitializer>();
    context.Database.Migrate();
    await dataInit.PopulateWordData();
}


app.UseRouting();
app.UseStaticFiles();
app.UseCookiePolicy();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();