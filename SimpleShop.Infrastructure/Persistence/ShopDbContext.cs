using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Modify.Interfaces;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Infrastructure.Persistence
{
    public class ShopDbContext : DbContext, IModifyShopContext
    {
        public DbSet<Order> Orders => Set<Order>();
        
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        public DbSet<Product> Products => Set<Product>();
        
        private string ConnectionString { get; }
        
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }

        public ShopDbContext(string connectionString)
        {
            ConnectionString = connectionString;
        }
        
        public void Delete<T>(T entity) where T : BaseEntity
        {
            Set<T>().Remove(entity);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(new SqlConnection(ConnectionString),
                builder => { builder.MigrationsAssembly(typeof(ShopDbContext).Assembly.FullName); });
        }
        
        public bool IsSqlServer()
        {
            return Database.IsSqlServer();
        }

        public async Task MigrateAsync()
        {
            await Database.MigrateAsync();
        }
    }
}
