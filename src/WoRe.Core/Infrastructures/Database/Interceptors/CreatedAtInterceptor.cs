using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WoRe.Core.Domain.Interfaces;


namespace WoRe.Core.Infrastructures.Database.Interceptors;

internal class CreatedAtInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync
        (DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var dataContext = eventData.Context;
        if (dataContext is null) return base.SavingChangesAsync(eventData, result, cancellationToken);

        var entries = dataContext
            .ChangeTracker
            .Entries<IAuditable>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
            .ToList();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.Property(a => a.UpdatedAt).CurrentValue = DateTime.UtcNow;
                    break;
                case EntityState.Added:
                    entry.Property(a => a.CreatedAt).CurrentValue = DateTime.UtcNow;
                    entry.Property(a => a.UpdatedAt).CurrentValue = DateTime.UtcNow;
                    break;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override InterceptionResult<int> SavingChanges
        (DbContextEventData eventData, InterceptionResult<int> result)
    {
        var dataContext = eventData.Context;
        if (dataContext is null) return base.SavingChanges(eventData, result);

        var entries = dataContext
            .ChangeTracker
            .Entries<IAuditable>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
            .ToList();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.Property(a => a.UpdatedAt).CurrentValue = DateTime.UtcNow;
                    break;
                case EntityState.Added:
                    entry.Property(a => a.CreatedAt).CurrentValue = DateTime.UtcNow;
                    entry.Property(a => a.UpdatedAt).CurrentValue = DateTime.UtcNow;
                    break;
            }
        }

        return base.SavingChanges(eventData, result);
    }
}
