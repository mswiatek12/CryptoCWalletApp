var builder = WebApplication.CreateBuilder(args);
{
    //scope for configuring services (Dependency Injection)
}

var app = builder.Build();
{
    //configure request pipeline
}

app.Run();