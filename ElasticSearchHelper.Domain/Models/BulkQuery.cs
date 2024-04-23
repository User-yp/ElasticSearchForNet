using ElasticSearchHelper.Domain.Interfaces;
using Nest;

namespace ElasticSearchHelper.Domain.Models;

public class BulkQuery<T> : IBulkQuery<T> where T : class
{
    public BulkDescriptor BulkDescriptor { get; set; }//构建批量操作请求的 BulkDescriptor 对象。

    public BulkQuery(string indexname)
    {
        BulkDescriptor = new BulkDescriptor(indexname);
        BulkDescriptor = this.BulkDescriptor.Index(indexname);
    }

    public void AddCollectionToSaveAnonymous(IEnumerable<T> items)
    {
        BulkDescriptor = this.BulkDescriptor.IndexMany(items);
    }

    public void AddCollectionToSave(IEnumerable<T> items, string id)
    {
        BulkDescriptor = this.BulkDescriptor.IndexMany(items, (descriptor, item) => descriptor.Id(BulkQuery<T>.GetIdValue(item, id)));
    }

    public void AddCollectionToDelete(IEnumerable<T> items)
    {
        BulkDescriptor = this.BulkDescriptor.DeleteMany(items);
    }

    public void AddCollectionToSave2(IEnumerable<T> items, string idPropertyName)
    {
        foreach (var item in items)
        {
            BulkDescriptor = this.BulkDescriptor.Index<T>(i => i
                .Document(item)
                .Id((Id)item.GetType().GetProperty(idPropertyName).GetValue(item))
            );
        }
    }

    private static Id GetIdValue(T item, string property)
    {
        var propertyInfo = item.GetType().GetProperty(property);
        if (!propertyInfo.PropertyType.IsValueType)
        {
            throw new InvalidOperationException($"Non-value type {propertyInfo.PropertyType.FullName} suggested for Nest Id casting.");
        }
        var test = new Id(propertyInfo.GetValue(item));
        return null;
    }

}