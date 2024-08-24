using Ecommerce.Models;
using Ecommerce.Models.Extensions;
using Ecommerce.Models.Providers;
using Ecommerce.ProductService.Infraestructure.EntityTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ecommerce.ProductService.Infraestructure;

public class ProductDbContext : DbContext
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IHttpContextAccessor _httpContext;

    public ProductDbContext(
        DbContextOptions<ProductDbContext> options, 
        IDateTimeProvider dateTimeProvider,
        IHttpContextAccessor httpContext) : base(options)
    {
        Database.EnsureCreated();
        _dateTimeProvider = dateTimeProvider;
        _httpContext = httpContext;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<Product>(new ProductEntityType());
        modelBuilder.ApplyConfiguration<Category>(new CategoryEntityType());

        modelBuilder.CreateSeeds();

        base.OnModelCreating(modelBuilder);
    }

    public override ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(
        TEntity entity, 
        CancellationToken cancellationToken = default)
    {
        if(entity is IEntity e)
        {
            e.CreatedAt = _dateTimeProvider.Now;
            e.CreatedBy = _httpContext.GetUserName() ?? "corrupt";
        }

        return base.AddAsync(entity, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach(var entry in ChangeTracker.Entries<IEntity>())
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
