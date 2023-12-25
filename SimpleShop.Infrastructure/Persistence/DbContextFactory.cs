using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SimpleShop.Infrastructure.Persistence;

//for migrations
public class DbContextFactory : IDesignTimeDbContextFactory<ShopDbContext>
{
    private static string DefaultConnectionString = @"Server=localhost\SQLEXPRESS;Database=SimpleShop.Modify;Trusted_Connection=True;";

    public ShopDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ShopDbContext>();

        optionsBuilder.UseSqlServer(DefaultConnectionString);

        return new ShopDbContext(optionsBuilder.Options);
    }
}