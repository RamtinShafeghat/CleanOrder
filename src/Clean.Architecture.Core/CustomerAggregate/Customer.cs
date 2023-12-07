using Clean.Architecture.SharedKernel.Interfaces;
using Clean.Architecture.SharedKernel;
using Ardalis.GuardClauses;

namespace Clean.Architecture.Core.CustomerAggregate;

public class Customer : EntityBase, IAggregateRoot
{
  public Customer(string name)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
  }
  
  public string Name { get; }
}
