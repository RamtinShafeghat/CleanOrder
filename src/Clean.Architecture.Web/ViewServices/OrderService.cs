using Ardalis.GuardClauses;
using Ardalis.Result;
using Clean.Architecture.Core.Interfaces;
using Clean.Architecture.Core.OrderAggregate;
using Clean.Architecture.Core.OrderAggregate.Specifications;
using Clean.Architecture.Core.OrderAggregate.Validators;
using Clean.Architecture.Core.ValueObjects;
using Clean.Architecture.SharedKernel.Interfaces;
using Clean.Architecture.Web.Endpoints.OrderEndpoints;
using Clean.Architecture.Web.Exceptions;

namespace Clean.Architecture.Web.ViewServices;

public class OrderService : IOrderService
{
  private readonly IOrderValidator _validator;
  private readonly IRepository<Order> _repository;
  private readonly IProductSearchService _productSearchService;

  public OrderService(
    IOrderValidator validator,
    IRepository<Order> repository,
    IProductSearchService productSearchService)
  {
    _validator = validator;
    _repository = repository;
    _productSearchService = productSearchService;
  }

  public async Task<GetOrderByIdResponse> GetOrderByItems(GetOrderByIdRequest request, CancellationToken ct)
  {
    var spec = new OrderByIdWithItemsSpec(request.OrderId);
    var order = await _repository.FirstOrDefaultAsync(spec, ct);
    if (order == default)
      throw new NotFoundException(request.OrderId.ToString(), nameof(request.OrderId));

    var productIds = order.Items.Select(a => a.ProductId).ToList();
    var productInfos = (await _productSearchService.GetProductInfos(productIds)).Value;
    
    order.UpdateTotalPrice(productInfos);
    order.UpdateShipmentMethod(productInfos);

    return new GetOrderByIdResponse(id: order.Id)
    {
      CustomerId = order.CustomerId,
      CreatedDate = order.CreatedDate,
      DiscountType = order.Discount.Type.ToString(),
      DiscountAmount = order.Discount.Amount,
      TotalPrice = order.TotalPrice, 
      ShipmentType = order.ShipmentType.ToString(),
      Items = order.Items.Select(a => new OrderItemDto
      (
        Name: productInfos[a.ProductId].Name,
        Quantity: a.Quantity,
        TotalPrice : a.TotalPrice
      )).ToList()
    };
  }

  public async Task<CreateOrderResponse> AddNewOrder(CreateOrderRequest request, CancellationToken ct)
  {
    var order = new Order(request.CustomerId);

    if (request.DiscountAmount > 0)
      order.Discount = new Discount(request.DiscountType, request.DiscountAmount);

    foreach (var item in request.Items)
      order.AddItem(new OrderItem(item.ProductId, item.Quantity));

    var productIds = request.Items.Select(a => a.ProductId).ToList();
    var productInfos = await _productSearchService.GetProductInfos(productIds);
    
    order.UpdateTotalPrice(productInfos);
    order.UpdateShipmentMethod(productInfos);

    var vResult = await _validator.Validate(order);
    if (vResult.Status != ResultStatus.Ok)
      throw new ValidationException(vResult.Errors);
    
    var createdOrder = await _repository.AddAsync(order, ct);

    return new CreateOrderResponse(createdOrder.Id)
    {
      TotalPrice = createdOrder.TotalPrice,
      ShipmentType = createdOrder.ShipmentType
    };
  }
}
