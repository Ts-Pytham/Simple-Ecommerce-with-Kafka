using MediatR;
using SimpleResults;

namespace Ecommerce.ProductService.Application.GetProductList;

public class GetProductListQuery : IRequest<ListedResult<GetProductListResponse>>
{
}

public class GetProductListResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public int Quantity { get; set; }
    public int CategoryId { get; set; }
}
