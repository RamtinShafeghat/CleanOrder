using Clean.Architecture.Core.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Architecture.Infrastructure.Data.Config;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
  public void Configure(EntityTypeBuilder<Product> builder)
  {
    builder.HasKey(t => t.Id);
    builder.Property(t => t.Name).IsRequired();
    builder.Property(t => t.Type).IsRequired();
    builder.Property(t => t.Price).HasColumnType("decimal(18, 0)").IsRequired();
  }
}
