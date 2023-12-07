using Ardalis.HttpClientTestExtensions;
using Clean.Architecture.Core.OrderAggregate;
using Clean.Architecture.Web;
using Clean.Architecture.Web.Endpoints.OrderEndpoints;
using Xunit;

namespace Clean.Architecture.FunctionalTests.ApiEndpoints;

[Collection("Sequential")]
public class OrderGetById : IClassFixture<CustomWebApplicationFactory<Program>>
{
  private readonly HttpClient _client;

  public OrderGetById(CustomWebApplicationFactory<Program> factory)
  {
    _client = factory.CreateClient();
  }

  [Fact]
  public async Task ReturnsSeedOrderGivenId1()
  {
    var result = await _client.GetAndDeserializeAsync<GetOrderByIdResponse>(GetOrderByIdRequest.BuildRoute(1));

    Assert.Equal(1, result.Id);
    Assert.Equal(SeedData.Order.CustomerId, result.CustomerId);
    Assert.Equal(SeedData.Order.Discount.Type.ToString(), result.DiscountType);
    Assert.Equal(SeedData.Order.Discount.Amount, result.DiscountAmount);
    Assert.Equal(665000, result.TotalPrice);
    Assert.Equal(OrderShipmentType.Regular.ToString(), result.ShipmentType);
    
    Assert.Equal(1, result?.Items.Count);
    Assert.Equal(SeedData.Order.Items.First().Quantity, result?.Items.First().Quantity);
  }

  [Fact]
  public async Task ReturnsNotFoundGivenId0()
  {
    string route = GetOrderByIdRequest.BuildRoute(0);
    _ = await _client.GetAndEnsureNotFoundAsync(route);
  }
}
