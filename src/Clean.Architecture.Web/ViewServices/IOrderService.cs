using Clean.Architecture.Web.Endpoints.OrderEndpoints;

namespace Clean.Architecture.Web.ViewServices;

public interface IOrderService
{
  Task<CreateOrderResponse> AddNewOrder(CreateOrderRequest request, CancellationToken ct);
  Task<GetOrderByIdResponse> GetOrderByItems(GetOrderByIdRequest request, CancellationToken ct);
}
