using Elasticsearch.Domain.Entity;
using Elasticsearch.Domain.IRepository;
using Nest;

namespace Elasticsearch.Infrastructure.Repository;

public class OrderRepository : ElasticBaseRepository<Order>, IOrderRepository
{
    public OrderRepository(IElasticClient elasticClient) : base(elasticClient)
    {
    }
    public override string IndexName { get; } = nameof(Order).ToLower();
}