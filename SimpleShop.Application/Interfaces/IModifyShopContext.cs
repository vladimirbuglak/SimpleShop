using SimpleShop.Domain.Entities;

namespace SimpleShop.Application.Modify.Interfaces;

public interface IModifyShopContext : IReadShopContext
{
    void Delete<T>(T entity) where T : BaseEntity;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}