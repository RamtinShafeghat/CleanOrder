using Ardalis.Result;

namespace Clean.Architecture.Core.OrderAggregate.Validators;

public interface IOrderValidator
{
  Task<Result> Validate(Order order);
}
