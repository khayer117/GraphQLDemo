using OrderDemo.Api.Models;

namespace OrderDemo.Api.GraphQL.Types;

public class OrderType : ObjectType<Order>
{
    protected override void Configure(IObjectTypeDescriptor<Order> descriptor)
    {
        descriptor.Field("totalAmount")
            .Type<DecimalType>()
            .Resolve(ctx =>
            {
                var order = ctx.Parent<Order>();
                return order.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice);
            });
    }
}
