using ElasticSearchHelper.Domain.Interfaces;
using Nest;

namespace ElasticSearchHelper.Domain.Models;

public class IndexQuery<T> : IIndexQuery<T> where T : class
{
    public IndexDescriptor<T> IndexQueryDescripter { get; set; }
    public CreateIndexDescriptor CreateIndexQueryDescripter { get; set; }
    protected TypeMappingDescriptor<T> TypeMappingDescriptor { get; set; }
    protected List<QueryContainer> Map { get; set; }

    public IndexQuery(string indexName)
    {
        TypeMappingDescriptor = new TypeMappingDescriptor<T>();

        CreateIndexQueryDescripter = new CreateIndexDescriptor(indexName);
        IndexQueryDescripter = new IndexDescriptor<T>();

        IndexQueryDescripter = IndexQueryDescripter.Index(indexName);
    }


    public void AutoMapIndex()
    {
        TypeMappingDescriptor = TypeMappingDescriptor.AutoMap();
        UpdateContainers();
    }

    public void DocumentId(Id id)
    {
        IndexQueryDescripter = IndexQueryDescripter.Id(id);
    }

    public void UpdateContainers()
    {
        CreateIndexQueryDescripter.Map<T>(m => TypeMappingDescriptor);
    }
}
