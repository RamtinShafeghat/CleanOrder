using Clean.Architecture.Core.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Architecture.Infrastructure.Data.Config;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
  public void Configure(EntityTypeBuilder<OrderItem> builder)
  {
    builder.HasKey(t => t.Id);
    builder.Property(t => t.Quantity).IsRequired();
    builder.Property(t => t.ProductId).IsRequired();
  }
}
