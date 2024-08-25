using Ecommerce.Shared.Models;
using Ecommerce.ProductService.Infraestructure;
using Ecommerce.Shared.Utilities.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleResults;

namespace Ecommerce.ProductService.Application.Categories.CreateCategory;

public class CreateCategoryHandler(ProductDbContext Context) 
    : IRequestHandler<CreateCategoryCommand, Result<CreatedId>>
{
    public async Task<Result<CreatedId>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        if ((await Context.Set<Category>()
                .AnyAsync(cat => EF.Functions.Like(cat.Name, request.Name), cancellationToken)) is true) 
        { 
            return Result.Failure(ValidationMessages.CategoryNameAlreadyExists);
        }

        var category = new Category
        {
            Name = request.Name,
            Description = request.Description
        };

        await Context.Set<Category>().AddAsync(category, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);

        return Result.Success(new CreatedId { Id = category.Id });
    }
}
