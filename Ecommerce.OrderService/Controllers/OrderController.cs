using Confluent.Kafka;
using Ecommerce.OrderService.Application.Orders.CreateOrder;
using Ecommerce.OrderService.Application.Orders.GetOrderList;
using Ecommerce.OrderService.Kafka;
using Ecommerce.Shared.Models.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SimpleResults;

namespace Ecommerce.OrderService.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class OrderController(IMediator Mediator, IKafkaProducer Producer)
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

        if (result.IsFailed)
        {
            return BadRequest(result);
        }

        //producer a message
        await Producer.ProduceAsync("order-topic", new Message<string, string>
        {
            Key = result.Data.Id.ToString(),
            Value = JsonConvert.SerializeObject(command)
        });

        return Ok(result);
    }
}
