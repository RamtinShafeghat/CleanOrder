using Ardalis.Specification;

namespace Clean.Architecture.Core.OrderAggregate.Specifications;

public class OpenOrderByCustomerIdSpec : Specification<Order>
{
  public OpenOrderByCustomerIdSpec(int customerId)
  {
    Query.Where(o => o.CustomerId == customerId && o.Status != OrderStatus.Completed);
  }
}
