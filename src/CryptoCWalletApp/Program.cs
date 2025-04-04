using System.Net;
using System.Security.Cryptography;
using CryptoCWalletApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<HttpClient>();

builder.Services.AddSingleton<CryptoService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



app.Run();