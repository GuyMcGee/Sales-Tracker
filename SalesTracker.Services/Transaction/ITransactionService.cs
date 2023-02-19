public interface ITransactionService
{
    Task<bool> CreateTransactionAsync(TransactionCreate transactionToCreate);
    Task<bool> DeleteTransactionAsync(int transactionId);
    Task<List<TransactionListItem>> GetAllTransactionsAsync();
    Task<TransactionDetails> GetTransactionByIdAsync(int transactionId);
    Task<bool> UpdateTransactionAsync(TransactionEdit request);
}