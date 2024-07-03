using Elasticsearch.Domain.Entity;
using Elasticsearch.Domain.IRepository;
using Elasticsearch.Domain.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Infrastructure.Service;

public class OrderService : IOrderService
{
    private readonly IOrderRepository orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }
    public async Task<ICollection<Order>> GetAllAsync()
    {
        var result = await orderRepository.GetAllAsync();

        return result.ToList();
    }

    public async Task<Order> GetOrderByIdAsync(Guid guid)
    {
        var result = await orderRepository.GetAsync(guid);

        if (result != null)
            return result;
        return null;
    }
}
