using Clean.Architecture.SharedKernel;

namespace Clean.Architecture.Core.OrderAggregate.Events;

public class NewOrderItemAddedEvent : DomainEventBase
{
  public NewOrderItemAddedEvent(Order order, OrderItem newItem)
  {
    Order = order;
    NewItem = newItem;
  }

  public Order Order { get; set; }
  public OrderItem NewItem { get; set; }
}
