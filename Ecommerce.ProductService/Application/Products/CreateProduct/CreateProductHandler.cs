using Ecommerce.Shared.Models;
using Ecommerce.ProductService.Infraestructure;
using MediatR;
using SimpleResults;

namespace Ecommerce.ProductService.Application.Products.CreateProduct;

public class CreateProductHandler(ProductDbContext Context) : IRequestHandler<CreateProductCommand, Result<CreatedId>>
{

    public async Task<Result<CreatedId>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Description = request.Description,
            Name = request.Name,
            CategoryId = request.CategoryId,
            ImageUrl = request.ImageUrl,
            Price = request.Price,
            Quantity = request.Quantity,
        };

        await Context.AddAsync(product, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);

        return Result.Success(new CreatedId { Id = product.Id });
    }
}
