using Ecommerce.Models;
using Ecommerce.ProductService.Infraestructure;
using Ecommerce.ProductService.Utilities.Constants;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleResults;
namespace Ecommerce.ProductService.Application.Products.CreateProduct;

public class CreateProductCommand : IRequest<Result<CreatedId>>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public int Quantity { get; set; }
    public int CategoryId { get; set; }
}

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator(ProductDbContext context)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Constants.Lengths.Name);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(Constants.Lengths.Description);

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.PriceGreaterThanZero);

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.ProductQuantityGreaterThanZero);

        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            .MustAsync(async (id, token)
                => await context.Set<Category>().AnyAsync(Category => Category.Id == id, token))
            .WithMessage(ValidationMessages.CategoryNotFound);

    }
}
