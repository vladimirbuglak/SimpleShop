using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SimpleShop.Application.Modify.Interfaces;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Infrastructure.Persistence
{
    public class EntitySaveChangesInterceptor : SaveChangesInterceptor
    {
        private IDateTime DateTime { get; }

        public EntitySaveChangesInterceptor(IDateTime dateTime)
        {
            DateTime = dateTime;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public void UpdateEntities(DbContext? context)
        {
            if (context == null) return;

            var utcNow = DateTime.UtcNow;

            foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreateOn = utcNow;
                }
            }
        }
    }
}