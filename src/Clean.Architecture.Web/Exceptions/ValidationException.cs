namespace Clean.Architecture.Web.Exceptions;

public class ValidationException : Exception
{
  public List<string> ValidationErrors { get; }

  public ValidationException(IEnumerable<string> erros)
  {
    this.ValidationErrors = erros.ToList();
  }
}
