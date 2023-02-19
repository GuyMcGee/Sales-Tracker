public class OrderListItem 
{
    public int Id { get; set; }
    public string location { get; set; }   
    public List<ProductListItem> Products { get; set; } = new List<ProductListItem>();

}