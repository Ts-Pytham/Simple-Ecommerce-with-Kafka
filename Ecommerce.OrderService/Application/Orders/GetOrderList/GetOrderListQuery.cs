using MediatR;
using SimpleResults;

namespace Ecommerce.OrderService.Application.Orders.GetOrderList;

public class GetOrderListQuery : IRequest<ListedResult<GetOrderListResponse>>
{
}


public class GetOrderListResponse
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
}
