using Clean.Architecture.SharedKernel.Interfaces;
using Clean.Architecture.SharedKernel;
using Ardalis.GuardClauses;
using System.ComponentModel.DataAnnotations.Schema;
using Clean.Architecture.Core.OrderAggregate.Events;
using Clean.Architecture.Core.ValueObjects;
using Clean.Architecture.Core.ProductAggregate;

namespace Clean.Architecture.Core.OrderAggregate;

public class Order : EntityBase, IAggregateRoot
{
  private readonly List<OrderItem> _items = new();

  public Order(int customerId)
  {
    this.CustomerId = Guard.Against.NegativeOrZero(customerId, nameof(customerId));

    this.CreatedDate = DateTime.Now;
    this.Status = OrderStatus.Created;
  }

  public int CustomerId { get; }
  public OrderStatus Status { get; }
  public DateTime CreatedDate { get; }
  public Discount Discount { get; set; } = Discount.Empty;
  public IEnumerable<OrderItem> Items => _items.AsReadOnly();

  [NotMapped]
  public decimal TotalPrice { get; private set; }
  [NotMapped]
  public OrderShipmentType ShipmentType { get; private set; }

  public void AddItem(OrderItem newItem)
  {
    Guard.Against.Null(newItem, nameof(newItem));
    _items.Add(newItem);

    var newItemAddedEvent = new NewOrderItemAddedEvent(this, newItem);
    base.RegisterDomainEvent(newItemAddedEvent);
  }

  public void UpdateTotalPrice(Dictionary<int, ProductInfo> productInfos)
  {
    foreach (var item in _items)
    {
      if (!productInfos.TryGetValue(item.ProductId, out var productInfo) || productInfo.Price == 0)
        throw new ArgumentNullException($"Price for ProductId:{item.ProductId} not found");

      item.TotalPrice = productInfo.Price * item.Quantity;
    }

    this.TotalPrice = _items.Sum(a => a.TotalPrice);

    if (this.Discount != Discount.Empty)
      this.TotalPrice = this.Discount.ApplyOn(this.TotalPrice);
  }
  public void UpdateShipmentMethod(Dictionary<int, ProductInfo> productInfos)
  {
    this.ShipmentType = OrderShipmentType.Regular;

    foreach (var item in _items)
    {
      if (!productInfos.TryGetValue(item.ProductId, out var productInfo))
        throw new ArgumentNullException($"Type for ProductId:{item.ProductId} not found");

      if (productInfo.Type == ProductType.Fragile)
      {
        this.ShipmentType = OrderShipmentType.Express;
        return;
      }
    }
  }
}
