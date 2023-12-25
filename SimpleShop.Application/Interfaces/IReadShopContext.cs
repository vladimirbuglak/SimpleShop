using Microsoft.EntityFrameworkCore;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Application.Modify.Interfaces;

public interface IReadShopContext
{
    DbSet<Order> Orders { get; }
        
    DbSet<Domain.Entities.Product> Products { get; }

    public bool IsSqlServer();

    public Task MigrateAsync();
}