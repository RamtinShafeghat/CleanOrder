using Ardalis.ApiEndpoints;
using Clean.Architecture.Web.ViewServices;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Clean.Architecture.Web.Endpoints.OrderEndpoints;

public class GetById : EndpointBaseAsync
  .WithRequest<GetOrderByIdRequest>
  .WithActionResult<GetOrderByIdResponse>
{
  private readonly IOrderService _orderService;

  public GetById(IOrderService orderService)
  {
    this._orderService = orderService;
  }

  [HttpGet(GetOrderByIdRequest.Route)]
  [SwaggerOperation(
    Summary = "Gets a single Order",
    Description = "Gets a single Order by Id",
    OperationId = "Orders.GetById",
    Tags = new[] { "OrderEndpoints" })
  ]
  public override async Task<ActionResult<GetOrderByIdResponse>> HandleAsync(
    [FromRoute] GetOrderByIdRequest request,
    CancellationToken cancellationToken = new())
  {
    var res = await _orderService.GetOrderByItems(request, cancellationToken);
    return Ok(res);
  }
}
