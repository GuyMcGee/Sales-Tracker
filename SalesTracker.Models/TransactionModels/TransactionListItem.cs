public class TransactionListItem
{
    public int Id { get; set; }
    public string PaymentMethod { get; set; }
    public DateTime DateOfTransaction { get; set; }
    public int CustomerId { get; set; }
    //public CustomerListItem Customer { get; set; }
    public string CustomerFullName { get; set; }
    // public OrderListItem Order { get; set; }
    public int OrderId { get; set; }
}