using Clean.Architecture.Core.OrderAggregate;
using Clean.Architecture.Core.ValueObjects;
using Xunit;

namespace Clean.Architecture.UnitTests.Core.OrderAggregate;

public class OrderConstructor
{
  private int _customerId = 1;
  private Order? _testOrder;
  private Order CreateOrder()
  {
    return new Order(_customerId);
  }

  [Fact]
  public void InitializesCustomerId()
  {
    _testOrder = CreateOrder();

    Assert.Equal(_customerId, _testOrder.CustomerId);
  }

  [Fact]
  public void InitializesStatus()
  {
    _testOrder = CreateOrder();

    Assert.Equal(OrderStatus.Created, _testOrder.Status);
  }

  [Fact]
  public void InitializesDiscountToEmpty()
  {
    _testOrder = CreateOrder();

    Assert.Equal(Discount.Empty, _testOrder.Discount);
  }

  [Fact]
  public void InitializesItemListToEmptyList()
  {
    _testOrder = CreateOrder();

    Assert.NotNull(_testOrder.Items);
  }
}
