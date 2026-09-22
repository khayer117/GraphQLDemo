using OrderDemo.Api.Models;

namespace OrderDemo.Api.GraphQL.Types;

public class OrderItemType : ObjectType<OrderItem>
{
    protected override void Configure(IObjectTypeDescriptor<OrderItem> descriptor)
    {
        descriptor.Field(oi => oi.Order).Ignore();
    }
}
