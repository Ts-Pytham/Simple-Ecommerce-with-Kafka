using Ecommerce.Shared.Models;
using Ecommerce.Shared.Utilities.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.ProductService.Infraestructure.EntityTypes;

public class CategoryEntityType : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories")
            .HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(Constants.Lengths.Name);

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(Constants.Lengths.Description);
    }
}
