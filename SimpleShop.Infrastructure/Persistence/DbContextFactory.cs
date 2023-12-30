using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SimpleShop.Infrastructure.Persistence;

//for migrations
public class DbContextFactory : IDesignTimeDbContextFactory<ShopDbContext>
{
    private static string DefaultConnectionString = @"Server=localhost\MSSQLSERVER01;Database=SimpleShop.Master;Trusted_Connection=True;";

    public ShopDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ShopDbContext>();

        optionsBuilder.UseSqlServer(DefaultConnectionString);

        return new ShopDbContext(optionsBuilder.Options);
    }
}