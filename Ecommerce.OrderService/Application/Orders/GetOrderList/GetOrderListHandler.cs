using Ecommerce.OrderService.Infraestructure;
using Ecommerce.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleResults;

namespace Ecommerce.OrderService.Application.Orders.GetOrderList;

public class GetOrderListHandler(OrderDbContext Context)
    : IRequestHandler<GetOrderListQuery, ListedResult<GetOrderListResponse>>
{
    public async Task<ListedResult<GetOrderListResponse>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
    {
        var list = await Context.Set<Order>()
            .Select(o => new GetOrderListResponse
            {
                Id              = o.Id,
                ProductId       = o.ProductId,
                Quantity        = o.Quantity,
                CustomerName    = o.CustomerName,
                OrderDate       = o.OrderDate
            })
            .ToListAsync(cancellationToken);

        return Result.ObtainedResources(list);
    }
}
