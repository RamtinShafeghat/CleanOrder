using Clean.Architecture.Core.Interfaces;
using Clean.Architecture.Core.ProductAggregate.Specifications;
using Clean.Architecture.Core.ProductAggregate;
using Clean.Architecture.SharedKernel.Interfaces;
using Ardalis.Result;

namespace Clean.Architecture.Core.Services;

public class ProductSearchService : IProductSearchService
{
  private readonly IReadRepository<Product> _repository;

  public ProductSearchService(IReadRepository<Product> _repository)
  {
    this._repository = _repository;
  }

  public async Task<Result<Dictionary<int, ProductInfo>>> GetProductInfos(IReadOnlyCollection<int> ids)
  {
    try
    {
      var spec = new ProductsByIdsSpec(ids);
      var products = (await _repository.ListAsync(spec)).ToDictionary(a => a.Id, a => new ProductInfo(a.Name, a.Price, a.Type));

      return new Result<Dictionary<int, ProductInfo>>(products);
    }
    catch (Exception ex)
    {
      return Result<Dictionary<int, ProductInfo>>.Error(new[] { ex.Message });
    }
  }
}
