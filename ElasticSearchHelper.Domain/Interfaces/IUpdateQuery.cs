namespace ElasticSearchHelper.Domain.Interfaces;

public interface IUpdateQuery<T> where T : class
{
    void UpdateDocument(object doc);
    void EnableElasticShardRefresh();
    void EnableDocAsUpsert();
}
