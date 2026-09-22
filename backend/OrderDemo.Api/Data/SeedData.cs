using OrderDemo.Api.Models;

namespace OrderDemo.Api.Data;

public static class SeedData
{
    public static void Initialize(AppDbContext db)
    {
        if (db.Customers.Any()) return;

        var customers = new List<Customer>
        {
            new() { Name = "Alice Johnson", Email = "alice@example.com" },
            new() { Name = "Bob Smith", Email = "bob@example.com" },
            new() { Name = "Carol White", Email = "carol@example.com" },
            new() { Name = "David Brown", Email = "david@example.com" },
            new() { Name = "Eve Davis", Email = "eve@example.com" },
        };
        db.Customers.AddRange(customers);

        var products = new List<Product>
        {
            new() { Name = "Wireless Keyboard", Price = 49.99m },
            new() { Name = "USB-C Hub", Price = 34.95m },
            new() { Name = "Monitor Stand", Price = 29.99m },
            new() { Name = "Mechanical Keyboard", Price = 119.00m },
            new() { Name = "Webcam HD", Price = 79.99m },
            new() { Name = "Mouse Pad XL", Price = 19.99m },
            new() { Name = "Laptop Stand", Price = 44.50m },
            new() { Name = "Cable Organizer", Price = 12.99m },
        };
        db.Products.AddRange(products);
        db.SaveChanges();

        var now = DateTime.UtcNow;
        var orders = new List<Order>
        {
            new() { CustomerId = customers[0].Id, OrderDate = now.AddDays(-2),
                OrderItems = new List<OrderItem>
                {
                    new() { ProductId = products[0].Id, Quantity = 1, UnitPrice = products[0].Price },
                    new() { ProductId = products[5].Id, Quantity = 2, UnitPrice = products[5].Price },
                }
            },
            new() { CustomerId = customers[1].Id, OrderDate = now.AddDays(-5),
                OrderItems = new List<OrderItem>
                {
                    new() { ProductId = products[3].Id, Quantity = 1, UnitPrice = products[3].Price },
                    new() { ProductId = products[2].Id, Quantity = 1, UnitPrice = products[2].Price },
                }
            },
            new() { CustomerId = customers[2].Id, OrderDate = now.AddDays(-7),
                OrderItems = new List<OrderItem>
                {
                    new() { ProductId = products[1].Id, Quantity = 2, UnitPrice = products[1].Price },
                }
            },
            new() { CustomerId = customers[0].Id, OrderDate = now.AddDays(-10),
                OrderItems = new List<OrderItem>
                {
                    new() { ProductId = products[4].Id, Quantity = 1, UnitPrice = products[4].Price },
                    new() { ProductId = products[6].Id, Quantity = 1, UnitPrice = products[6].Price },
                    new() { ProductId = products[7].Id, Quantity = 3, UnitPrice = products[7].Price },
                }
            },
            new() { CustomerId = customers[3].Id, OrderDate = now.AddDays(-14),
                OrderItems = new List<OrderItem>
                {
                    new() { ProductId = products[0].Id, Quantity = 2, UnitPrice = products[0].Price },
                    new() { ProductId = products[3].Id, Quantity = 1, UnitPrice = products[3].Price },
                }
            },
            new() { CustomerId = customers[4].Id, OrderDate = now.AddDays(-20),
                OrderItems = new List<OrderItem>
                {
                    new() { ProductId = products[5].Id, Quantity = 1, UnitPrice = products[5].Price },
                }
            },
            new() { CustomerId = customers[1].Id, OrderDate = now.AddDays(-25),
                OrderItems = new List<OrderItem>
                {
                    new() { ProductId = products[2].Id, Quantity = 2, UnitPrice = products[2].Price },
                    new() { ProductId = products[6].Id, Quantity = 1, UnitPrice = products[6].Price },
                }
            },
            new() { CustomerId = customers[2].Id, OrderDate = now.AddDays(-30),
                OrderItems = new List<OrderItem>
                {
                    new() { ProductId = products[1].Id, Quantity = 1, UnitPrice = products[1].Price },
                    new() { ProductId = products[4].Id, Quantity = 2, UnitPrice = products[4].Price },
                }
            },
            new() { CustomerId = customers[3].Id, OrderDate = now.AddDays(-35),
                OrderItems = new List<OrderItem>
                {
                    new() { ProductId = products[7].Id, Quantity = 4, UnitPrice = products[7].Price },
                    new() { ProductId = products[0].Id, Quantity = 1, UnitPrice = products[0].Price },
                }
            },
            new() { CustomerId = customers[4].Id, OrderDate = now.AddDays(-40),
                OrderItems = new List<OrderItem>
                {
                    new() { ProductId = products[3].Id, Quantity = 2, UnitPrice = products[3].Price },
                }
            },
            new() { CustomerId = customers[0].Id, OrderDate = now.AddDays(-45),
                OrderItems = new List<OrderItem>
                {
                    new() { ProductId = products[6].Id, Quantity = 1, UnitPrice = products[6].Price },
                    new() { ProductId = products[1].Id, Quantity = 3, UnitPrice = products[1].Price },
                    new() { ProductId = products[5].Id, Quantity = 2, UnitPrice = products[5].Price },
                }
            },
            new() { CustomerId = customers[2].Id, OrderDate = now.AddDays(-50),
                OrderItems = new List<OrderItem>
                {
                    new() { ProductId = products[2].Id, Quantity = 1, UnitPrice = products[2].Price },
                    new() { ProductId = products[7].Id, Quantity = 2, UnitPrice = products[7].Price },
                }
            },
        };
        db.Orders.AddRange(orders);
        db.SaveChanges();
    }
}
