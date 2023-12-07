using Ardalis.GuardClauses;
using Clean.Architecture.SharedKernel;

namespace Clean.Architecture.Core.ValueObjects;

public class Discount : ValueObject
{
  public static Discount Empty = new();

  private Discount()
  {
      // EF
  }
  public Discount(DiscountType type, decimal amount)
  {
    this.Type = type;
    this.Amount = Guard.Against.NegativeOrZero(amount);
  }

  public DiscountType Type { get; }
  public decimal Amount { get; }

  internal decimal ApplyOn(decimal totalPrice)
  {
    var result = totalPrice;

    switch (this.Type)
    {
      case DiscountType.Value:
        result = totalPrice -= this.Amount;
        break;
      case DiscountType.Percentage:
        result = ((100 - this.Amount) * totalPrice) / 100;
        break;
      default:
        throw new NotSupportedException("Discount Type not supported");
    }

    return result;
  }

  protected override IEnumerable<object> GetEqualityComponents()
  {
    yield return Type;
    yield return Amount;
  }
}
