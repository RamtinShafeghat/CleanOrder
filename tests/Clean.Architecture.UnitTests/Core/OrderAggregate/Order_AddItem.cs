using Clean.Architecture.Core.OrderAggregate;
using Clean.Architecture.Core.OrderAggregate.Events;
using Xunit;

namespace Clean.Architecture.UnitTests.Core.OrderAggregate;

public class Order_AddItem
{
  private readonly Order _order = new(1);

  [Fact]
  public void AddsItemToItems()
  {
    var _testItem = new OrderItem(1, 1000);
    _order.AddItem(_testItem);

    Assert.Contains(_testItem, _order.Items);
  }

  [Fact]
  public void ThrowsExceptionGivenNullItem()
  {
    #nullable disable
    Action action = () => _order.AddItem(null);
    #nullable enable

    var ex = Assert.Throws<ArgumentNullException>(action);
    Assert.Equal("newItem", ex.ParamName);
  }

  [Fact]
  public void RaisesOrderItemAddedEvent()
  {
    var _testItem = new OrderItem(1, 1000);
    _order.AddItem(_testItem);

    Assert.Single(_order.DomainEvents);
    Assert.IsType<NewOrderItemAddedEvent>(_order.DomainEvents.First());
  }
}
