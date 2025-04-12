using System.Net;
using CryptoCWalletApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "CryptoCWalletApp API",
        Version = "v1",
        Description = "API for Crypto Wallet App"
    });
});

builder.Services.AddSingleton<HttpClient>();

builder.Services.AddScoped<CryptoService>();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHostedService<CryptoBackgroundService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!dbContext.Database.CanConnect())
    {
        throw new NotImplementedException("Can't connect to database");
    }
    else
    {
        Console.WriteLine("Connected to DB");
    }
}

app.MapControllers();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CryptoCWalletApp API V1");
    c.RoutePrefix = string.Empty;
});

app.Run();