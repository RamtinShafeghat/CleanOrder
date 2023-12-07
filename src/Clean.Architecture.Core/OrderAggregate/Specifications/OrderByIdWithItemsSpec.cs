using Ardalis.Specification;

namespace Clean.Architecture.Core.OrderAggregate.Specifications;

public class OrderByIdWithItemsSpec : Specification<Order>
{
  public OrderByIdWithItemsSpec(int orderId)
  {
    Query.Where(order => order.Id == orderId).Include(order => order.Items);
  }
}
