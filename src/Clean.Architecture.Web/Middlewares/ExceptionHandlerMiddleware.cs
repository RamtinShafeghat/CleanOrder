using Ardalis.GuardClauses;
using Clean.Architecture.Web.Exceptions;
using System.Net;
using System.Text.Json;

namespace Clean.Architecture.Web.Middlewares;

public class ExceptionHandlerMiddleware
{
  private readonly RequestDelegate _next;

  public ExceptionHandlerMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task Invoke(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (Exception ex)
    {
      await ConvertException(context, ex);
    }
  }

  private Task ConvertException(HttpContext context, Exception exception)
  {
    var (code, errors) = GetErrors(exception);


    var response = JsonSerializer.Serialize(new
    {
      Code = code,
      Result = errors,
    });

    context.Response.ContentType = "application/json";
    context.Response.StatusCode = (int)code;
    return context.Response.WriteAsync(response);
  }

  private static (HttpStatusCode code, object errors) GetErrors(Exception exception)
  {
    return exception switch
    {
      ValidationException validationException => (HttpStatusCode.BadRequest, validationException.ValidationErrors),
      NotSupportedException or ArgumentNullException _ => (HttpStatusCode.BadRequest, exception.Message),
      NotFoundException _ => (HttpStatusCode.NotFound, exception.Message),
      _ => (HttpStatusCode.InternalServerError, exception.Message)
    };
  }
}
