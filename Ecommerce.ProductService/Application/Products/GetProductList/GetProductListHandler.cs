using Ecommerce.Shared.Models;
using Ecommerce.ProductService.Infraestructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleResults;
using Ecommerce.Shared.Models.Products;

namespace Ecommerce.ProductService.Application.Products.GetProductList;

public class GetProductListHandler(ProductDbContext Context)
    : IRequestHandler<GetProductListQuery, ListedResult<GetProductListResponse>>
{
    public async Task<ListedResult<GetProductListResponse>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        var list = await Context.Set<Product>()
            .Select(p => new GetProductListResponse
            {
                Description = p.Description,
                CategoryId = p.CategoryId,
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity
            })
            .ToListAsync(cancellationToken);

        return Result.ObtainedResources(list);
    }
}
