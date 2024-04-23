using Elasticsearch.Net;
using Nest;

namespace ElasticSearchHelper.Infrastructure.Services;

public class ElasticSearchService : IElasticSearchService
{
    public IElasticClient _elasticClient { get; set; }

    public ElasticSearchService(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    public async Task<IApiCallDetails> TestConnectionAsync()
    {
        var result = await _elasticClient.PingAsync();
        return result.ApiCall;
    }

    public async Task<IApiCallDetails> IndexAsync<T>(T item, IndexDescriptor<T> indexDescriptor) where T : class
    {
        var result = await _elasticClient.IndexAsync(item, indexDescriptor => indexDescriptor);
        return result.ApiCall;
    }
    public async Task<IApiCallDetails> IndexAsync<T>(T item, string indexName) where T : class
    {
        var result = await _elasticClient.IndexAsync(item,d=>d.Index(indexName).Id(Guid.NewGuid()));
        return result.ApiCall;
    }
    public async Task<IApiCallDetails> CreateIndexAsync(string indexname, CreateIndexDescriptor createIndexDescriptor)
    {
        var result = await _elasticClient.Indices.CreateAsync(indexname, cid => createIndexDescriptor);
        return result.ApiCall;
    }

    public async Task<IApiCallDetails> BulkAsync(BulkDescriptor BulkDescriptor)
    {
        var result = await _elasticClient.BulkAsync(BulkDescriptor);
        return result.ApiCall;
    }
    public async Task<IApiCallDetails> BulkAsync(BulkDescriptor BulkDescriptor, string indexName)
    {
        var result = await _elasticClient.BulkAsync(BulkDescriptor.Index(indexName));
        return result.ApiCall;
    }

    public async Task<IApiCallDetails> UpdateAsync<T>(UpdateDescriptor<T, object> updateDescriptor) where T : class
    {
        var result = await _elasticClient.UpdateAsync(updateDescriptor);
        return result.ApiCall;
    }

    public async Task<IApiCallDetails> UpdateAsync<T>(T model,string indexName ,Id id) where T : class
    {
        var result = await _elasticClient.UpdateAsync(DocumentPath<T>.Id(id).Index(indexName), p => p.Doc(model));
        return result.ApiCall;
    }
    public async Task<IEnumerable<T>> SearchAsync<T>(SearchDescriptor<T> searchDescriptor) where T : class
    {
        var result = await _elasticClient.SearchAsync<T>(searchDescriptor);
        return result.Documents;
    }
    public async Task<IEnumerable<T>> SearchAsync<T>(Func<QueryContainerDescriptor<T>, QueryContainer> query) where T : class
    {
        var result = await _elasticClient.SearchAsync<T>(s => s.Index(typeof(T).Name.ToLower()).Query(query));
        return result.Documents;
    }
}
