


public class OrderDetails 
{
    public int Id { get; set; }
    public string Location { get; set; }
    public List<ProductListItem> Products { get; set; } = new List<ProductListItem>();
}