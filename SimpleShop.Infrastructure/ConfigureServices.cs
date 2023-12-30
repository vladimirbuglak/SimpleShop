using Amazon.S3;
using KafkaFlow;
using KafkaFlow.Serializer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleShop.Application.Modify.Events;
using SimpleShop.Application.Modify.Interfaces;
using SimpleShop.Infrastructure.AWS;
using SimpleShop.Infrastructure.Common;
using SimpleShop.Infrastructure.Kafka;
using SimpleShop.Infrastructure.Persistence;

namespace SimpleShop.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStrings = configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
            
            services.AddScoped<IReadShopContext>(provider => new ShopDbContext(connectionStrings.ReadConnectionString));
            
            services.AddScoped<IModifyShopContext>(provider => new ShopDbContext(connectionStrings.ModifyConnectionString));
            
            services.AddScoped<IDateTime, DateTimeService>();

            services.AddTransient<IDbInitializer, DbInitializer>();

            services.AddTransient<IServiceBus, ServiceBus>();
            
            services.AddKafka(kafka => kafka
                .AddCluster(cluster => cluster
                    .WithBrokers(new[] { "localhost:9092" })
                    .CreateTopicIfNotExists(EventTopics.Mapping[typeof(ProductCreated)].TopicName, 1, 1)
                    .AddProducer(EventTopics.Mapping[typeof(ProductCreated)].Name, producer => producer
                        .DefaultTopic(EventTopics.Mapping[typeof(ProductCreated)].TopicName)
                        .AddMiddlewares(middlewares => middlewares
                            .AddSerializer<JsonCoreSerializer>()
                        )
                    )
                    .CreateTopicIfNotExists(EventTopics.Mapping[typeof(ProductUpdatedEvent)].TopicName, 1, 1)
                    .AddProducer(EventTopics.Mapping[typeof(ProductUpdatedEvent)].Name, producer => producer
                        .DefaultTopic(EventTopics.Mapping[typeof(ProductUpdatedEvent)].TopicName)
                        .AddMiddlewares(middlewares => middlewares
                            .AddSerializer<JsonCoreSerializer>()
                        )
                    )
                    .CreateTopicIfNotExists(EventTopics.Mapping[typeof(OrderCreated)].TopicName, 1, 1)
                    .AddProducer(EventTopics.Mapping[typeof(OrderCreated)].Name, producer => producer
                        .DefaultTopic(EventTopics.Mapping[typeof(OrderCreated)].TopicName)
                        .AddMiddlewares(middlewares => middlewares
                            .AddSerializer<JsonCoreSerializer>()
                        )
                    )
                    .CreateTopicIfNotExists(EventTopics.Mapping[typeof(OrderCancelled)].TopicName, 1, 1)
                    .AddProducer(EventTopics.Mapping[typeof(OrderCancelled)].Name, producer => producer
                        .DefaultTopic(EventTopics.Mapping[typeof(OrderCancelled)].TopicName)
                        .AddMiddlewares(middlewares => middlewares
                            .AddSerializer<JsonCoreSerializer>()
                        )
                    )
                )
            );
            
            services.AddSingleton<AwsS3Config>(x => configuration.GetSection("AwsS3Config").Get<AwsS3Config>());
            
            services.AddTransient<IFilesStorage, AwsS3Service>();

            return services;
        }
    }
}
