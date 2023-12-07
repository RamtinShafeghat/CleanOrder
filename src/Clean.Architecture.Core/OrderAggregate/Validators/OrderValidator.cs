using Ardalis.Result;
using Clean.Architecture.Core.OrderAggregate.Specifications;
using Clean.Architecture.SharedKernel.Interfaces;

namespace Clean.Architecture.Core.OrderAggregate.Validators;

public class OrderValidator : IOrderValidator
{
  private readonly IReadRepository<Order> _repository;

  public OrderValidator(IReadRepository<Order> repository)
  {
    _repository = repository;
  }

  public async Task<Result> Validate(Order order)
  {
    if (!order.Items.Any())
      return Result.Error("Must have atleast one item");

    if (!(order.CreatedDate.Hour >= 8 || order.CreatedDate.Hour <= 19))
      return Result.Error("Out of allowed time");

    if (order.TotalPrice <= 50000)
      return Result.Error("Minimum order amount is 50000");

    if (await HasOpenOrder(order))
      return Result.Error("There is an already open order for customer");

    return Result.Success();
  }

  private async Task<bool> HasOpenOrder(Order order)
  {
    var orderSpec = new OpenOrderByCustomerIdSpec(order.CustomerId);
    var existed = await _repository.FirstOrDefaultAsync(orderSpec);
    return existed != default && (order.Id == default || order.Id != existed.Id);
  }
}
