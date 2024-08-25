using Ecommerce.OrderService.Infraestructure.EntityTypes;
using Ecommerce.Shared.Extensions;
using Ecommerce.Shared.Models;
using Ecommerce.Shared.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ecommerce.OrderService.Infraestructure;

public class OrderDbContext : DbContext
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IHttpContextAccessor _httpContext;

    public OrderDbContext(
        DbContextOptions<OrderDbContext> options,
        IDateTimeProvider dateTimeProvider,
        IHttpContextAccessor httpContext) : base(options)
    {
        Database.EnsureCreated();
        _dateTimeProvider = dateTimeProvider;
        _httpContext = httpContext;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<Order>(new OrderEntityType());
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }
    public override ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        if (entity is IEntity e)
        {
            e.CreatedAt = _dateTimeProvider.Now;
            e.CreatedBy = _httpContext.GetUserName() ?? "corrupt";
        }

        return base.AddAsync(entity, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<IEntity>())
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = _dateTimeProvider.Now;
                entry.Entity.UpdatedBy = _httpContext.GetUserName() ?? "corrupt";
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
