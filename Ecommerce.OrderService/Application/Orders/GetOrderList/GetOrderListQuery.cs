using Ecommerce.Shared.Models.Orders;
using MediatR;
using SimpleResults;

namespace Ecommerce.OrderService.Application.Orders.GetOrderList;

public class GetOrderListQuery : IRequest<ListedResult<GetOrderListResponse>>
{
}