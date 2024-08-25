using Ecommerce.Shared.Models;
using Ecommerce.ProductService.Infraestructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleResults;

namespace Ecommerce.ProductService.Application.Categories.GetCategoriesList;

public class GetCategoriesListHandler(ProductDbContext Context)
    : IRequestHandler<GetCategoriesListQuery, ListedResult<GetCategoriesListResponse>>
{
    public async Task<ListedResult<GetCategoriesListResponse>> Handle(
        GetCategoriesListQuery request, 
        CancellationToken cancellationToken)
    {
        var list = await Context.Set<Category>()
            .Select(c => new GetCategoriesListResponse
            {
                Id              = c.Id,
                Name            = c.Name,
                Description     = c.Description
            })
            .ToListAsync(cancellationToken);

        return Result.ObtainedResources(list);
    }
}
