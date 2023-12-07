using Clean.Architecture.Core.OrderAggregate;
using Clean.Architecture.Core.ValueObjects;

namespace Clean.Architecture.UnitTests;

public class OrderBuilder
{
  private readonly Order _order = new(1);
  private readonly OrderItem _item = new(1, 10);

  public OrderBuilder WithDefaultValues()
  {
    _order.AddItem(_item);
    
    return this;
  }

  public OrderBuilder WithAnotherItem(OrderItem item)
  {
    _order.AddItem(item);

    return this;
  }

  public OrderBuilder WithValueDiscount(decimal amount)
  {
    _order.Discount = new Discount(DiscountType.Value, amount);

    return this;
  }

  public OrderBuilder WithPercentageDiscount(decimal percent)
  {
    _order.Discount = new Discount(DiscountType.Percentage, percent);

    return this;
  }

  public Order Build() => _order;
}
