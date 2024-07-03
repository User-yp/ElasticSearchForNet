using Elasticsearch.Domain.Entity;

namespace Elasticsearch.Domain.IService;

public interface IOrderService
{
    Task<ICollection<Order>> GetAllAsync();
    Task<Order> GetOrderByIdAsync(Guid guid);
}
