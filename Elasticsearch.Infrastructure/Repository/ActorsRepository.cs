using Elasticsearch.Domain.IRepository;
using Nest;
using Elasticsearch.Domain.Entity;

namespace Elasticsearch.Infrastructure.Repository;

public class ActorsRepository : ElasticBaseRepository<Actors>, IActorsRepository
{

    public ActorsRepository(IElasticClient elasticClients) : base(elasticClients)
    {
    }
    public override string IndexName { get; } = nameof(Actors).ToLower();

}
