using ElasticSearchHelper.Domain.Interfaces;
using Nest;

namespace ElasticSearchHelper.Domain.Models;

public class SearchQuery<T> : ISearchQuery<T> where T : class
{
    public SearchDescriptor<T> QueryDescripter { get; set; }
    protected QueryContainer BaseQueryContainer { get; set; }
    protected BoolQuery BoolQuery { get; set; }
    protected List<QueryContainer> BoolMust { get; set; }
    protected List<QueryContainer> BoolShould { get; set; }
    protected List<QueryContainer> BoolMustNot { get; set; }
    protected List<QueryContainer> BoolShouldNot { get; set; }
    protected IdsQuery IdsQuery { get; set; }

    public SearchQuery(string indexName)
    {
        BaseQueryContainer = new QueryContainer();
        BoolQuery = new BoolQuery();
        BoolMust = new List<QueryContainer>();
        BoolShould = new List<QueryContainer>();

        IdsQuery = new IdsQuery();

        QueryDescripter = new SearchDescriptor<T>();
        QueryDescripter.Index(indexName);
        UpdateContainers();
    }

    public void AddMustMatchConditon<G>(Field field, G value)
    {
        BoolMust.Add(
            new MatchQuery()
            {
                Field = field,
                Query = value.ToString()
            }
        );

        UpdateContainers();
    }

    public void AddShouldMatchCondtion<G>(Field field, G value)
    {
        BoolShould.Add(
            new MatchQuery()
            {
                Field = field,
                Query = value.ToString()
            }
        );

        UpdateContainers();
    }

    public void AddMustNotMatchCondtion<G>(Field field, G value)
    {
        BoolMustNot.Add(
            new MatchQuery()
            {
                Field = field,
                Query = value.ToString()
            }
        );

        UpdateContainers();
    }

    public void AddDocIds(params Id[] values)
    {
        IdsQuery = new IdsQuery()
        {
            Values = values
        };

        UpdateContainers();
    }

    public void UpdateContainers()
    {
        BoolQuery.Must = BoolMust;
        BoolQuery.Should = BoolShould;
        BoolQuery.MustNot = BoolMustNot;

        BaseQueryContainer &= BoolQuery;
        BaseQueryContainer &= IdsQuery;
        QueryDescripter.Query(q => BaseQueryContainer);
    }
}
