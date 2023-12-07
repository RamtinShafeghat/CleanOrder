using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using Clean.Architecture.SharedKernel;

namespace Clean.Architecture.Core.OrderAggregate;

public class OrderItem : EntityBase
{
  public OrderItem() { }

  public OrderItem(int productId, int quantity)
  {
    this.ProductId = Guard.Against.NegativeOrZero(productId, nameof(productId));
    this.Quantity = Guard.Against.NegativeOrZero(quantity, nameof(quantity));
  }

  public int ProductId { get; }
  public int Quantity { get; }
  
  [NotMapped]
  public decimal TotalPrice { get; set; }
}
