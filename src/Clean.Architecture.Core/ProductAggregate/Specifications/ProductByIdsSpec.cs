using Ardalis.Specification;

namespace Clean.Architecture.Core.ProductAggregate.Specifications;

public class ProductsByIdsSpec : Specification<Product>
{
  public ProductsByIdsSpec(IReadOnlyCollection<int> ids)
  {
    Query.Where(item => ids.Contains(item.Id));
  }
}
