using Clean.Architecture.Core.OrderAggregate;
using Clean.Architecture.Core.ProductAggregate;
using Xunit;

namespace Clean.Architecture.UnitTests.Core.OrderAggregate;

public class Order_UpdateShipmentType
{
  private readonly Dictionary<int, ProductInfo> _productInfos = new()
  {
    { 1, new ProductInfo("test", 3000, ProductType.Ordinary) },
    { 2, new ProductInfo("test", 3000, ProductType.Ordinary) },
    { 3, new ProductInfo("test", 3000, ProductType.Fragile) }
  };

  [Fact]
  public void SetShipmentWhenAllProductIsOrdinary()
  {
    var order = new OrderBuilder()
                  .WithDefaultValues()
                  .WithAnotherItem(new OrderItem(2, 10))
                  .Build();

    order.UpdateShipmentMethod(_productInfos);

    Assert.Equal(OrderShipmentType.Regular, order.ShipmentType);
  }

  [Fact]
  public void SetShipmentWhenOneProductIsFragile()
  {
    var order = new OrderBuilder()
                  .WithDefaultValues()
                  .WithAnotherItem(new OrderItem(3, 10))
                  .Build();

    order.UpdateShipmentMethod(_productInfos);

    Assert.Equal(OrderShipmentType.Express, order.ShipmentType);
  }
}
