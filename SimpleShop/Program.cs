using KafkaFlow;
using SimpleShop.Application.Modify;
using SimpleShop.Infrastructure;
using SimpleShop.Infrastructure.Persistence;
using SimpleShop.ModifyApi.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services
    .AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>())
    .AddNewtonsoftJson();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();

    await dbInitializer.MigrateAsync();
}

var kafkaBus = app.Services.CreateKafkaBus();

app.Lifetime.ApplicationStarted.Register(async (o, token) =>
{
    await kafkaBus.StartAsync();
}, null);


app.Lifetime.ApplicationStopped.Register(async (o, token) =>
{
    await kafkaBus.StopAsync();
}, null);


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
