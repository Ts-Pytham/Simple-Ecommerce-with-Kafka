using Ecommerce.OrderService.Infraestructure;
using Ecommerce.Shared.Models;
using Ecommerce.Shared.Providers;
using MediatR;
using SimpleResults;

namespace Ecommerce.OrderService.Application.Orders.CreateOrder;

public class CreateOrderHandler(OrderDbContext Context, IDateTimeProvider DateTimeProvider)
    : IRequestHandler<CreateOrderCommand, Result<CreatedId>>
{
    public async Task<Result<CreatedId>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            CustomerName = request.CustomerName,
            OrderDate = DateTimeProvider.Now
        };

        await Context.Set<Order>().AddAsync(order, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);

        return Result.Success(new CreatedId{ Id = order.Id });
    }
}
