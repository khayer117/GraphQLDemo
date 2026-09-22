using Microsoft.EntityFrameworkCore;
using OrderDemo.Api.Data;
using OrderDemo.Api.GraphQL.Filters;
using OrderDemo.Api.Models;

namespace OrderDemo.Api.GraphQL;

public class Query
{
    public async Task<IEnumerable<Order>> GetOrders(
        [Service] AppDbContext db,
        OrdersFilterInput? filter)
    {
        IQueryable<Order> query = db.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems);

        if (filter is not null)
        {
            if (filter.OrderId.HasValue)
                query = query.Where(o => o.Id == filter.OrderId.Value);

            if (filter.CustomerId.HasValue)
                query = query.Where(o => o.CustomerId == filter.CustomerId.Value);

            if (filter.OrderDateFrom.HasValue)
                query = query.Where(o => o.OrderDate >= filter.OrderDateFrom.Value);
        }

        var orders = await query.ToListAsync();

        // minAmount is computed in-memory because totalAmount isn't a stored column.
        // This is a simplification acceptable for a small dataset; at scale you'd want
        // a denormalized total column or a SQL-side aggregation.
        if (filter?.MinAmount.HasValue == true)
            orders = orders
                .Where(o => o.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice) >= filter.MinAmount.Value)
                .ToList();

        return orders;
    }

    public async Task<Order?> GetOrderById([Service] AppDbContext db, int id)
    {
        return await db.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id);
    }
}
