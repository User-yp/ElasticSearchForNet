using Nest;

namespace ElasticSearchHelper.Domain.Interfaces;

public interface ISearchQuery<T> where T : class
{
    void AddMustMatchConditon<G>(Field field, G value);
    void AddShouldMatchCondtion<G>(Field field, G value);
    void AddMustNotMatchCondtion<G>(Field field, G value);
    void AddDocIds(params Id[] values);
    void UpdateContainers();
}
