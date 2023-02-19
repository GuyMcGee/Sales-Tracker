public class TransactionCreate
{
    // public List<int> OrderIdList { get; set; } //How do we associate this list of ints with OrderEntities?

    public string PaymentMethod { get; set; }

    public DateTime DateOfTransaction { get; set; }

    public int CustomerId { get; set; }
    public int OrderId { get; set; }
}