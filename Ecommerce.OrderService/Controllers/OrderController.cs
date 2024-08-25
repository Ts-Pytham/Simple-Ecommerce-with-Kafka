using Ecommerce.OrderService.Application.Orders.GetOrderList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleResults;

namespace Ecommerce.OrderService.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class OrderController(IMediator Mediator)
    : ControllerBase
{
    [HttpGet("list")]
    public async Task<ActionResult<ListedResult<GetOrderListResponse>>> GetOrderList()
    {
        var result = await Mediator.Send(new GetOrderListQuery());
        return Ok(result);
    }
}
