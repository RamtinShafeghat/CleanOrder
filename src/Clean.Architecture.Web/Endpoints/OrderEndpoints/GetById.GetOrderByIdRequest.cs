
namespace Clean.Architecture.Web.Endpoints.OrderEndpoints;

public class GetOrderByIdRequest
{
  public const string Route = "/Orders/{OrderId:int}";
  public static string BuildRoute(int OrderId) => Route.Replace("{OrderId:int}", OrderId.ToString());

  public int OrderId { get; set; }
}
