using Ecommerce.Models;
using Ecommerce.ProductService.Utilities.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.ProductService.Infraestructure.EntityTypes;

public class ProductEntityType : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products", table =>
        {
            table.HasCheckConstraint("CK_Products_Price", "Price >= 0");
            table.HasCheckConstraint("CK_Products_Quantity", "Quantity > 0");
        })
        .HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(Constants.Lengths.Name);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(Constants.Lengths.Description);

        builder.Property(p => p.Price)
            .IsRequired();

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(p => p.Price)
            .IsRequired();

        builder.Property(p => p.Quantity)
            .IsRequired();

    }
}
