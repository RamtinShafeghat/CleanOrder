using Clean.Architecture.Core.OrderAggregate;
using Clean.Architecture.Core.OrderAggregate.Specifications;
using Clean.Architecture.Core.ValueObjects;
using Xunit;

namespace Clean.Architecture.IntegrationTests.Data;

public class EfRepositoryAdd : BaseEfRepoTestFixture
{
  [Fact]
  public async Task AddsOrderAndSetsId()
  {
    var customerId = 1;
    var discountAmount = 5;
    var order = new Order(customerId)
    {
      Discount = new Discount(DiscountType.Percentage, discountAmount)
    };

    var productId = 1;
    var productQuantity = 1000;
    order.AddItem(new OrderItem(productId, productQuantity));

    var repository = GetRepository();
    var createdId = (await repository.AddAsync(order)).Id;

    var createdOrder = (await repository.ListAsync(new OrderByIdWithItemsSpec(createdId))).FirstOrDefault();

    Assert.True(createdOrder?.Id > 0);
    Assert.Equal(customerId, createdOrder?.CustomerId);
    Assert.Equal(DiscountType.Percentage, createdOrder?.Discount.Type);
    Assert.Equal(discountAmount, createdOrder?.Discount.Amount);
    
    Assert.Equal(productId, createdOrder?.Items.First().ProductId);
    Assert.Equal(productQuantity, createdOrder?.Items.First().Quantity);
  }
}
