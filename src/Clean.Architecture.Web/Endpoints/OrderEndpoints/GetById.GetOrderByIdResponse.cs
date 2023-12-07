namespace Clean.Architecture.Web.Endpoints.OrderEndpoints;

public class GetOrderByIdResponse
{
  public GetOrderByIdResponse(int id)
  {
    Id = id;
  }

  public int Id { get; set; }
  public int CustomerId { get; set; }
  public DateTime CreatedDate { get; set; }
  public string DiscountType { get; set; } = string.Empty;
  public decimal DiscountAmount { get; set; }
  public decimal TotalPrice { get; set; }
  public string ShipmentType { get; set; } = string.Empty;
  public List<OrderItemDto> Items { get; set; } = new();
}

public record class OrderItemDto(string Name, int Quantity, decimal TotalPrice);

