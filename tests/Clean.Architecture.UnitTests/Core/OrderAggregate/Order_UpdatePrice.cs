using Clean.Architecture.Core.ProductAggregate;
using Xunit;

namespace Clean.Architecture.UnitTests.Core.OrderAggregate;

public class Order_UpdatePrice
{
  private readonly Dictionary<int, ProductInfo> _productInfos = new()
  {
    { 1, new ProductInfo("test", 3000, ProductType.Fragile) }
  };

  [Fact]
  public void SetsTotalPriceWithoutDiscount()
  {
    var order = new OrderBuilder()
                  .WithDefaultValues()
                  .Build();

    order.UpdateTotalPrice(_productInfos);

    Assert.Equal(30000, order.TotalPrice);
    Assert.Equal(30000, order.Items.First().TotalPrice);
  }

  [Fact]
  public void SetsTotalPriceWithValueDiscount()
  {
    var order = new OrderBuilder()
                  .WithDefaultValues()
                  .WithValueDiscount(1000)
                  .Build();

    order.UpdateTotalPrice(_productInfos);

    Assert.Equal(29000, order.TotalPrice);
    Assert.Equal(30000, order.Items.First().TotalPrice);
  }

  [Fact]
  public void SetsTotalPriceWithPercentageDiscount()
  {
    var order = new OrderBuilder()
                  .WithDefaultValues()
                  .WithPercentageDiscount(10)
                  .Build();

    order.UpdateTotalPrice(_productInfos);

    Assert.Equal(27000, order.TotalPrice);
    Assert.Equal(30000, order.Items.First().TotalPrice);
  }
}
