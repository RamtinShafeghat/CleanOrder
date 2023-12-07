using System.ComponentModel.DataAnnotations;
using Clean.Architecture.Core.ValueObjects;

namespace Clean.Architecture.Web.Endpoints.OrderEndpoints;

public class CreateOrderRequest
{
  public const string Route = "/Orders";

  [Required]
  public int CustomerId { get; set; }

  public DiscountType DiscountType { get; set; }

  public decimal DiscountAmount { get; set; }

  public OrderItemRequest[] Items { get; set; } = Array.Empty<OrderItemRequest>();
}

public class OrderItemRequest
{
  public int ProductId { get; set; }

  public int Quantity { get; set; }
}


