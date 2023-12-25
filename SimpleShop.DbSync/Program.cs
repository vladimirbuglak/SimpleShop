using KafkaFlow;
using KafkaFlow.Serializer;
using SimpleShop.Application.Modify;
using SimpleShop.DbSync.Handlers;
using SimpleShop.Infrastructure;
using SimpleShop.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructureServices(builder.Configuration);

var connectionStrings = builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

builder.Services.AddScoped<ShopDbContext>(provider => new ShopDbContext(connectionStrings.ReadConnectionString));

builder.Services.AddKafka(kafka => kafka
    .AddCluster(cluster => cluster
        .WithBrokers(new[] {"localhost:9092"})
        .CreateTopicIfNotExists("product-created", 1, 1)
        .AddConsumer(consumer => consumer
            .Topic("product-created")
            .WithAutoOffsetReset(AutoOffsetReset.Earliest)
            .WithGroupId("products-sync")
            .WithBufferSize(1)
            .WithWorkersCount(1)
            .AddMiddlewares(middlewares => middlewares
                .AddDeserializer<JsonCoreDeserializer>()
                .AddTypedHandlers(handlers => handlers
                    .AddHandler<ProductCreatedHandler>()
                    .WithHandlerLifetime(InstanceLifetime.Scoped))
            )
        )
        .CreateTopicIfNotExists("product-updated", 1, 1)
        .AddConsumer(consumer => consumer
            .Topic("product-updated")
            .WithAutoOffsetReset(AutoOffsetReset.Earliest)
            .WithGroupId("products-updated-sync")
            .WithBufferSize(1)
            .WithWorkersCount(1)
            .AddMiddlewares(middlewares => middlewares
                .AddDeserializer<JsonCoreDeserializer>()
                .AddTypedHandlers(handlers => handlers
                    .AddHandler<ProductUpdatedHandler>()
                    .WithHandlerLifetime(InstanceLifetime.Scoped))
            )
        )
        .CreateTopicIfNotExists("order-created", 1, 1)
        .AddConsumer(consumer => consumer
            .Topic("order-created")
            .WithAutoOffsetReset(AutoOffsetReset.Earliest)
            .WithGroupId("orders-created-sync")
            .WithBufferSize(1)
            .WithWorkersCount(1)
            .AddMiddlewares(middlewares => middlewares
                .AddDeserializer<JsonCoreDeserializer>()
                .AddTypedHandlers(handlers => handlers
                    .AddHandler<OrderCreatedHandler>()
                    .WithHandlerLifetime(InstanceLifetime.Scoped))
            )
        )
        .CreateTopicIfNotExists("order-cancelled", 1, 1)
        .AddConsumer(consumer => consumer
            .Topic("order-cancelled")
            .WithAutoOffsetReset(AutoOffsetReset.Earliest)
            .WithGroupId("orders-cancelled-sync")
            .WithBufferSize(1)
            .WithWorkersCount(1)
            .AddMiddlewares(middlewares => middlewares
                .AddDeserializer<JsonCoreDeserializer>()
                .AddTypedHandlers(handlers => handlers
                    .AddHandler<OrderCancelledHandler>()
                    .WithHandlerLifetime(InstanceLifetime.Scoped))
            )
        )
    ));
    
var app = builder.Build();

var kafkaBus = app.Services.CreateKafkaBus();

app.Lifetime.ApplicationStarted.Register(async (o, token) =>
{
    await kafkaBus.StartAsync();
}, null);


app.Lifetime.ApplicationStopped.Register(async (o, token) =>
{
    await kafkaBus.StopAsync();
}, null);

app.Run();
