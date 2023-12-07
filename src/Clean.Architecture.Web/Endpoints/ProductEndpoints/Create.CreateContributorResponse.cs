namespace Clean.Architecture.Web.Endpoints.ProductEndpoints;

public class CreateProductResponse
{
  public CreateProductResponse(int id)
  {
    Id = id;
  }

  public int Id { get; set; }
}
