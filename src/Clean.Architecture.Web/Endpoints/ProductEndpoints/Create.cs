using Clean.Architecture.Core.ProductAggregate;
using Clean.Architecture.SharedKernel.Interfaces;
using FastEndpoints;

namespace Clean.Architecture.Web.Endpoints.ProductEndpoints;

public class Create : Endpoint<CreateProductRequest, CreateProductResponse>
{
  private readonly IRepository<Product> _repository;

  public Create(IRepository<Product> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Post(CreateProductRequest.Route);
    AllowAnonymous();
    Options(x => x
      .WithTags("ProductEndpoints"));
  }
  public override async Task HandleAsync(
    CreateProductRequest request,
    CancellationToken cancellationToken)
  {
    if (string.IsNullOrEmpty(request.Name))
      ThrowError("Name is required");

    var newProduct = new Product(request.Name, request.Price, request.Type);
    var createdProduct = await _repository.AddAsync(newProduct, cancellationToken);
    
    var response = new CreateProductResponse
    (
      id: createdProduct.Id
    );

    await SendAsync(response, cancellation: cancellationToken);
  }
}
