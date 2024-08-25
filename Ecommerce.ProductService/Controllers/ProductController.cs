using Ecommerce.ProductService.Application.Products.CreateProduct;
using Ecommerce.ProductService.Application.Products.GetProductList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleResults;

namespace Ecommerce.ProductService.Controllers;
[Route("api/[controller]s")]
[ApiController]
public class ProductController(IMediator Mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Result<CreatedId>>> CreateProduct(CreateProductCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("list")]
    public async Task<ActionResult<ListedResult<GetProductListResponse>>> GetProductList()
    {
        var result = await Mediator.Send(new GetProductListQuery());
        return Ok(result);
    }
}
