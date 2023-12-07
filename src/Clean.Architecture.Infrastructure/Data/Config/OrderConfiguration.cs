using System.Reflection.Emit;
using System.Reflection.Metadata;
using Clean.Architecture.Core.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Architecture.Infrastructure.Data.Config;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
  public void Configure(EntityTypeBuilder<Order> builder)
  {
    builder.HasKey(t => t.Id);
    builder.Property(t => t.CreatedDate).IsRequired();
    builder.Property(t => t.Status).IsRequired();
    builder.Property(t => t.CustomerId).IsRequired();
    builder.OwnsOne(t => t.Discount, t =>
    {
      t.Property(tt => tt.Type).HasColumnName("Discount_Type");
      t.Property(tt => tt.Amount).HasColumnType("decimal(18, 0)").HasColumnName("Discount_Amount");
    });
  }
}
