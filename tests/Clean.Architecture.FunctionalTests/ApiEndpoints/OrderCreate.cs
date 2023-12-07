using Ardalis.HttpClientTestExtensions;
using Xunit;
using FluentAssertions;
using Clean.Architecture.Web.Endpoints.OrderEndpoints;

namespace Clean.Architecture.FunctionalTests.ApiEndpoints;

[Collection("Sequential")]
public class OrderCreate : IClassFixture<CustomWebApplicationFactory<Program>>
{
  private readonly HttpClient _client;

  public OrderCreate(CustomWebApplicationFactory<Program> factory)
  {
    _client = factory.CreateClient();
  }

  [Fact]
  public async Task ReturnsOneOrder()
  {
    var request = new CreateOrderRequest() { 
      CustomerId = 2,
      DiscountType = Core.ValueObjects.DiscountType.Percentage,
      DiscountAmount = 5,
      Items = new[] { new OrderItemRequest
                      {
                        ProductId = 1,
                        Quantity = 1000
                      } 
                    }
    };

    var content = StringContentHelpers.FromModelAsJson(request);

    var result = await _client.PostAndDeserializeAsync<CreateOrderResponse>(CreateOrderRequest.Route, content);

    result.Id.Should().BePositive();
    result.TotalPrice.Should().Be(665000);
  }
}

