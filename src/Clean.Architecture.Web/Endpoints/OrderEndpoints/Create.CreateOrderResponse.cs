using Clean.Architecture.Core.OrderAggregate;

namespace Clean.Architecture.Web.Endpoints.OrderEndpoints;

public class CreateOrderResponse
{
  public CreateOrderResponse(int id)
  {
    this.Id = id;
  }

  public int Id { get; set; }
  public decimal TotalPrice { get; set; }
  public OrderShipmentType ShipmentType { get; set; }
}
