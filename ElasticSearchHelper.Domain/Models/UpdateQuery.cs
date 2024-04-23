using ElasticSearchHelper.Domain.Interfaces;
using Nest;

namespace ElasticSearchHelper.Domain.Models;

public class UpdateQuery<T> : IUpdateQuery<T> where T : class
{
    public UpdateDescriptor<object, object> QueryDescripter { get; set; }

    public UpdateQuery(string indexName, Nest.Id id)
    {
        QueryDescripter = new UpdateDescriptor<object, object>(id);
        QueryDescripter = QueryDescripter.Index(indexName);
    }

    public void UpdateDocument(object doc)
    {
        QueryDescripter = QueryDescripter.Doc(doc);
    }

    public void EnableElasticShardRefresh()
    {
        QueryDescripter = QueryDescripter.Refresh(Elasticsearch.Net.Refresh.True);
    }

    public void EnableDocAsUpsert()
    {
        QueryDescripter = QueryDescripter.DocAsUpsert();
    }
}
