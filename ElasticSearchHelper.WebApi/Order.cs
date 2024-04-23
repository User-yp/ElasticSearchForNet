namespace ElasticSearchHelper.WebApi;

public class Order
{
    public Guid Guid { get; set; }
    public Guid TableId { get; set; }
    public Guid OrderId { get; set; }
    public string TableName { get; set; }
    public string ProjectText { get; set; }
}
