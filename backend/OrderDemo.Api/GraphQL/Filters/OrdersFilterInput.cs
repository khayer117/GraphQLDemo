namespace OrderDemo.Api.GraphQL.Filters;

public class OrdersFilterInput
{
    public int? OrderId { get; set; }
    public int? CustomerId { get; set; }
    public decimal? MinAmount { get; set; }
    public DateTime? OrderDateFrom { get; set; }
}
