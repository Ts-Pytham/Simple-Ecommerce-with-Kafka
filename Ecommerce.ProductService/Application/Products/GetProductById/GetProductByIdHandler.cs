using Ecommerce.ProductService.Infraestructure;
using Ecommerce.Shared.Models;
using Ecommerce.Shared.Models.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleResults;

namespace Ecommerce.ProductService.Application.Products.GetProductById;

public class GetProductByIdHandler(ProductDbContext Context) 
    : IRequestHandler<GetProductByIdQuery, Result<GetProductListResponse>>
{
    public async Task<Result<GetProductListResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await Context.Set<Product>()
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product is null) return Result.NotFound();

        return Result.Success(new GetProductListResponse
        {
            Id              = product.Id,
            Name            = product.Name,
            Description     = product.Description,
            Price           = product.Price,
            CategoryId      = product.CategoryId,
            ImageUrl        = product.ImageUrl,
            Quantity        = product.Quantity
        });
    }
}
