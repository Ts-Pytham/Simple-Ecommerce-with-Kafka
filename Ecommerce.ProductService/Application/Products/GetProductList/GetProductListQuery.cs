using Ecommerce.Shared.Models.Products;
using MediatR;
using SimpleResults;

namespace Ecommerce.ProductService.Application.Products.GetProductList;

public class GetProductListQuery : IRequest<ListedResult<GetProductListResponse>>
{
}
