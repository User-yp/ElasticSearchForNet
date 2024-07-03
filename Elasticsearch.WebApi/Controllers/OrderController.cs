using Elasticsearch.Domain.Entity;
using Elasticsearch.Domain.IService;
using Microsoft.AspNetCore.Mvc;

namespace Elasticsearch.WebApi.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService orderService;

    public OrderController(IOrderService orderService)
    {
        this.orderService = orderService;
    }
    [HttpGet]
    public async Task<ActionResult<Order>> GetAllOrder()
    {
        var result = await orderService.GetAllAsync();

        return Ok(result);
    }
    [HttpGet("guid")]
    public async Task<ActionResult<Order>> GetAllOrderById(Guid guid)
    {
        var result = await orderService.GetOrderByIdAsync(guid);

        return Ok(result);
    }
}
