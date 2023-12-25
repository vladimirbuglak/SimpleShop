using SimpleShop.Application.Modify.Interfaces;

namespace SimpleShop.Infrastructure.Persistence;

public interface IDbInitializer
{
    Task MigrateAsync();
}

public class DbInitializer : IDbInitializer
{
    public IReadShopContext ReadShopContext { get; set; }
    
    public IModifyShopContext ModifyShopContext { get; set; }
    

    public DbInitializer(IReadShopContext readShopContext, IModifyShopContext modifyShopContext)
    {
        ReadShopContext = readShopContext ?? throw new ArgumentNullException(nameof(readShopContext));
        ModifyShopContext = modifyShopContext ?? throw new ArgumentNullException(nameof(modifyShopContext));
    }

    public async Task MigrateAsync()
    {
        if (ReadShopContext.IsSqlServer())
            await ReadShopContext.MigrateAsync();

        if (ModifyShopContext.IsSqlServer())
            await ModifyShopContext.MigrateAsync();
    }
}