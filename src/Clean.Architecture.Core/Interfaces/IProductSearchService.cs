using Ardalis.Result;
using Clean.Architecture.Core.ProductAggregate;

namespace Clean.Architecture.Core.Interfaces;

public interface IProductSearchService
{
  Task<Result<Dictionary<int, ProductInfo>>> GetProductInfos(IReadOnlyCollection<int> ids);
}
