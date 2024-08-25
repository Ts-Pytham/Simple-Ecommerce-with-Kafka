using Ecommerce.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.OrderService.Infraestructure.EntityTypes;

public class OrderEntityType : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders")
            .HasKey(o => o.Id);

        builder.Property(o => o.Quantity)
            .IsRequired();

        builder.Property(o => o.CustomerName)
            .IsRequired();

        builder.Property(o => o.ProductId)
            .IsRequired();

        builder.Property(o => o.OrderDate)
            .IsRequired();
    }
}
