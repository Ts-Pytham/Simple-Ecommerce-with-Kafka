using Ecommerce.Models;
using Ecommerce.ProductService.Infraestructure;
using Ecommerce.ProductService.Utilities.Constants;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleResults;

namespace Ecommerce.ProductService.Application.Categories.CreateCategory;

public class CreateCategoryCommand : IRequest<Result<CreatedId>>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class CreateCategoryValidator 
    : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(Constants.Lengths.Name);

        RuleFor(c => c.Description)
            .MaximumLength(Constants.Lengths.Description);
    }
}
