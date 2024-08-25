using FluentValidation;
using MediatR;
using SimpleResults;

namespace Ecommerce.OrderService.Application.Orders.CreateOrder;

public class CreateOrderCommand : Ecommerce.Shared.Models.Orders.CreateOrder, IRequest<Result<CreatedId>>
{

}

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0);

        RuleFor(x => x.Quantity)
            .GreaterThan(0);

        RuleFor(x => x.CustomerName)
            .NotEmpty();
    }
}
