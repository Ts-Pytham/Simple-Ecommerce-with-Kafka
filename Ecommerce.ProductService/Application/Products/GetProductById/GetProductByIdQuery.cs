using Ecommerce.Shared.Models.Products;
using FluentValidation;
using MediatR;
using SimpleResults;

namespace Ecommerce.ProductService.Application.Products.GetProductById;

public class GetProductByIdQuery : IRequest<Result<GetProductListResponse>>
{
    public int Id { get; set; }
}

public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}
