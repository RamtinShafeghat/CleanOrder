using Clean.Architecture.Core.CustomerAggregate;
using Clean.Architecture.Core.OrderAggregate;
using Clean.Architecture.Core.ProductAggregate;
using Clean.Architecture.Core.ValueObjects;
using Clean.Architecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Clean.Architecture.Web;

public static class SeedData
{
  public static readonly Customer[] Customers = new Customer[] { 
    new("Reza"), new("Ahmad"), new("Mohsen") 
  };
  
  public static readonly Product[] Products = new Product[] {
    new("Mini Desk", 700, ProductType.Ordinary),
    new("White Blanket", 150, ProductType.Ordinary),
    new("Oral Toothbrush", 10, ProductType.Ordinary),
    new("Asus Laptop Zenbook 13", 1500, ProductType.Fragile),
    new("Asus Laptop Zenbook 14", 2000, ProductType.Fragile),
    new("Asus Laptop Zenbook Pro", 3000, ProductType.Fragile),
  };

  public static readonly Order Order = new(1) { Discount = new Discount(DiscountType.Percentage, 5) };
  public static readonly OrderItem OrderItem = new(1, 1000);

  public static void Initialize(IServiceProvider serviceProvider)
  {
    using var dbContext = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null);
    
    if (dbContext.Customers.Any())
      return;

    PopulateTestData(dbContext);
  }

  public static void PopulateTestData(AppDbContext dbContext)
  {
    RemoveOldOnes(dbContext);
    AddNewOnes(dbContext);
  }
  private static void RemoveOldOnes(AppDbContext dbContext)
  {
    foreach (var item in dbContext.Orders)
      dbContext.Remove(item);

    foreach (var item in dbContext.OrderItems)
      dbContext.Remove(item);

    foreach (var item in dbContext.Customers)
      dbContext.Remove(item);

    foreach (var item in dbContext.Products)
      dbContext.Remove(item);

    dbContext.SaveChanges();
  }
  private static void AddNewOnes(AppDbContext dbContext)
  {
    dbContext.Customers.AddRange(Customers);
    dbContext.Products.AddRange(Products);

    dbContext.SaveChanges();

    Order.AddItem(OrderItem);
    dbContext.Orders.Add(Order);

    dbContext.SaveChanges();
  }
}
