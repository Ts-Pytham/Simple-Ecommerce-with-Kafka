using MediatR;
using SimpleResults;

namespace Ecommerce.ProductService.Application.Categories.GetCategoriesList;

public class GetCategoriesListQuery : IRequest<ListedResult<GetCategoriesListResponse>>
{
}

public class GetCategoriesListResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
