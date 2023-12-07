using Clean.Architecture.SharedKernel.Interfaces;
using Clean.Architecture.SharedKernel;
using Ardalis.GuardClauses;

namespace Clean.Architecture.Core.ProductAggregate;

public class Product : EntityBase, IAggregateRoot
{
  public Product() { }

  public Product(string name, decimal price, ProductType type)
  {
    this.Name = Guard.Against.NullOrEmpty(name, nameof(name));
    this.Price = Guard.Against.NegativeOrZero(price, nameof(price));
    Type = type;
  }

  public string Name { get; } = string.Empty;
  public decimal Price { get; }
  public ProductType Type { get; }
}
