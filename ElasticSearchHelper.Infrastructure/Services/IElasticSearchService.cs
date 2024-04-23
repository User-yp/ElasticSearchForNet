using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchHelper.Infrastructure.Services;

public interface IElasticSearchService
{
    Task<IApiCallDetails> TestConnectionAsync();
    Task<IApiCallDetails> IndexAsync<T>(T item, IndexDescriptor<T> indexDescriptor) where T : class;
    Task<IApiCallDetails> IndexAsync<T>(T item, string indexName) where T : class;
    Task<IApiCallDetails> CreateIndexAsync(string indexname, CreateIndexDescriptor createIndexDescriptor);
    Task<IApiCallDetails> BulkAsync(BulkDescriptor BulkDescriptor);
    Task<IApiCallDetails> BulkAsync(BulkDescriptor BulkDescriptor, string indexName);
    Task<IApiCallDetails> UpdateAsync<T>(UpdateDescriptor<T, object> updateDescriptor) where T : class;
    Task<IApiCallDetails> UpdateAsync<T>(T model, string indexName, Id id) where T : class;
    Task<IEnumerable<T>> SearchAsync<T>(SearchDescriptor<T> searchDescriptor) where T : class;
    Task<IEnumerable<T>> SearchAsync<T>(Func<QueryContainerDescriptor<T>, QueryContainer> query) where T : class;
}
