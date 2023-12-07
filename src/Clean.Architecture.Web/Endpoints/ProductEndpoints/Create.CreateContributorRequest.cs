using System.ComponentModel.DataAnnotations;
using Clean.Architecture.Core.ProductAggregate;

namespace Clean.Architecture.Web.Endpoints.ProductEndpoints;

public class CreateProductRequest
{
  public const string Route = "/Products";

  [Required]
  public string Name { get; set; } = string.Empty;

  [Required]
  public decimal Price { get; set; }

  [Required]
  public ProductType Type { get; set; }
}
