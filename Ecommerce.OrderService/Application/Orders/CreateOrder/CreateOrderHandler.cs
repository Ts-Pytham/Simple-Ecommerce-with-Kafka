using Ecommerce.OrderService.Infraestructure;
using Ecommerce.Shared.Models;
using Ecommerce.Shared.Models.Products;
using Ecommerce.Shared.Providers;
using MediatR;
using SimpleResults;

namespace Ecommerce.OrderService.Application.Orders.CreateOrder;

public class CreateOrderHandler(OrderDbContext Context, IDateTimeProvider DateTimeProvider, HttpClient Client)
    : IRequestHandler<CreateOrderCommand, Result<CreatedId>>
{
    public async Task<Result<CreatedId>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var product = await Client.GetFromJsonAsync<Result<GetProductListResponse>>($"https://localhost:7160/api/Products/{request.ProductId}", cancellationToken);

        if (product!.IsFailed)
        {
            return Result.Failure(product.Message);
        }

        if (product.Data.Quantity - request.Quantity < 0)
        {
            return Result.Failure("Product out of stock");
        }

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
