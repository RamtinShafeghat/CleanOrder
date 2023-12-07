using Ardalis.ApiEndpoints;
using Clean.Architecture.Web.ViewServices;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Clean.Architecture.Web.Endpoints.OrderEndpoints;

public class Create : EndpointBaseAsync
  .WithRequest<CreateOrderRequest>
  .WithActionResult<CreateOrderResponse>
{
  private readonly IOrderService _orderService;

  public Create(IOrderService orderService)
  {
    this._orderService = orderService;
  }

  [HttpPost("/Orders")]
  [SwaggerOperation(
    Summary = "Creates a new Order",
    Description = "Creates a new Order",
    OperationId = "Order.Create",
    Tags = new[] { "OrderEndpoints" })
  ]
  public override async Task<ActionResult<CreateOrderResponse>> HandleAsync(
    CreateOrderRequest request,
    CancellationToken cancellationToken = new())
  {
    if (request.CustomerId == default)
      return BadRequest("CustomerId is required");

    var res = await _orderService.AddNewOrder(request, cancellationToken);

    return Ok(res);
  }
}
