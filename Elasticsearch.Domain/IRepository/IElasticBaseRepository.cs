﻿using Elasticsearch.Net;
using Nest;

namespace Elasticsearch.Domain.IRepository;

public interface IElasticBaseRepository<T> where T : class
{
    Task<IApiCallDetails> TestConnectionAsync();
    Task<T> GetAsync(Guid id);
    Task<T> GetAsync(IGetRequest request);
    Task<T> FindAsync(Guid id);
    Task<T> FindAsync(IGetRequest request);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetManyAsync(IEnumerable<string> ids);
    Task<IEnumerable<T>> SearchAsync(Func<SearchDescriptor<T>, ISearchRequest> selector);
    Task<IEnumerable<T>> SearchAsync(Func<QueryContainerDescriptor<T>, QueryContainer> request);
    Task<ISearchResponse<T>> SearchAsync(Func<QueryContainerDescriptor<T>, QueryContainer> request, Func<AggregationContainerDescriptor<T>, IAggregationContainer> aggregationsSelector);
    Task<IEnumerable<T>> SearchInAllFields(string term);
    Task<bool> CreateIndexAsync();
    Task<bool> InsertAsync(T t);
    Task<bool> InsertManyAsync(IList<T> tList);
    Task<bool> UpdateAsync(T t);
    Task<bool> UpdatePartAsync(T t, object partialEntity);
    Task<long> GetTotalCountAsync();
    Task<bool> DeleteByIdAsync(string id);
    Task<bool> DeleteByQueryAsync(Func<QueryContainerDescriptor<T>, QueryContainer> selector);
    Task<bool> ExistAsync(string id);
}
