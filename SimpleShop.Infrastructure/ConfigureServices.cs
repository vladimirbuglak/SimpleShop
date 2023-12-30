using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            
            services.AddSingleton<AwsS3Config>(x => configuration.GetSection("AwsS3Config").Get<AwsS3Config>());
            
            services.AddTransient<IFilesStorage, AwsS3Service>();

            return services;
        }
    }
}
