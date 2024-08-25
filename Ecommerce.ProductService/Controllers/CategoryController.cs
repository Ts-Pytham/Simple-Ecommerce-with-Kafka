using Ecommerce.ProductService.Application.Categories.CreateCategory;
using Ecommerce.ProductService.Application.Categories.GetCategoriesList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleResults;

namespace Ecommerce.ProductService.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoryController(IMediator Mediator) : ControllerBase
{
    [HttpGet("list")]
    public async Task<ActionResult<ListedResult<GetCategoriesListResponse>>> GetCategoriesList()
    {
        var result = await Mediator.Send(new GetCategoriesListQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Result<CreatedId>>> CreateCategory(CreateCategoryCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}
