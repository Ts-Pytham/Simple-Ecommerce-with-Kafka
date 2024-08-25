using Ecommerce.OrderService.Application.Orders.CreateOrder;
using Ecommerce.OrderService.Application.Orders.GetOrderList;
using Ecommerce.Shared.Models.Orders;
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

    [HttpPost]
    public async Task<ActionResult<Result<CreatedId>>> CreateOrder(CreateOrderCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}
