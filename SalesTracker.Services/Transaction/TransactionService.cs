using Microsoft.EntityFrameworkCore;


public class TransactionService : ITransactionService //this IService is only for ease of testing, unless you're writing libraries and stuff or more sophisticated code
{
    private readonly AppDbContext _context;

    public TransactionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateTransactionAsync(TransactionCreate transactionToCreate)
    {
        var entity = new TransactionEntity
        {
            OrderId = transactionToCreate.OrderId,
            CustomerId = transactionToCreate.CustomerId,
            PaymentMethod = transactionToCreate.PaymentMethod,
            DateOfTransaction = transactionToCreate.DateOfTransaction
        };

        await _context.Transactions.AddAsync(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<List<TransactionListItem>> GetAllTransactionsAsync()
    {
        return await _context.Transactions.Select(entity => new TransactionListItem
        {
            Id = entity.Id,
            PaymentMethod = entity.PaymentMethod,
            DateOfTransaction = DateTime.Now,
            CustomerFullName = entity.Customer.FullName,
            OrderId = entity.Order.Id,
            CustomerId = entity.CustomerId
        }).ToListAsync();
    }
    public async Task<TransactionDetails> GetTransactionByIdAsync(int transactionId)
    {
        var transactionFromDatabase = await _context.Transactions
    .Include(t => t.Customer)
    .Include(t => t.Order)
    .ThenInclude(t => t.Products)
    .FirstOrDefaultAsync(entity => entity.Id == transactionId);

        return transactionFromDatabase is null ? null : new TransactionDetails
        {
            Id = transactionFromDatabase.Id,
            PaymentMethod = transactionFromDatabase.PaymentMethod,
            DateOfTransaction = transactionFromDatabase.DateOfTransaction,
            Customer = new CustomerListItem
            {
                Id = transactionFromDatabase.Customer.Id,
                FullName = transactionFromDatabase.Customer.FullName
            },
            Order = new OrderListItem
            {
                Id = transactionFromDatabase.Order.Id,
                location = transactionFromDatabase.Order.location,
                Products = transactionFromDatabase.Order.Products.Select(p => new ProductListItem
                {
                    Id = p.Id,
                    Name = p.Name,
                    Cost = p.Cost
                }).ToList()
            }
        };
    }

    public async Task<bool> UpdateTransactionAsync(TransactionEdit request)
    {
        var transactionToBeUpdated = await _context.Transactions.FindAsync(request.Id);

        if (transactionToBeUpdated == null)
            return false;

        transactionToBeUpdated.OrderId = request.OrderId;
        transactionToBeUpdated.PaymentMethod = request.PaymentMethod;
        transactionToBeUpdated.CustomerId = request.CustomerId;

        var numberOfChanges = await _context.SaveChangesAsync();
        return numberOfChanges == 1;
    }

    public async Task<bool> DeleteTransactionAsync(int transactionId)
    {
        var transactionEntity = await _context.Transactions.
        FindAsync(transactionId);

        if (transactionEntity == null)
            return false;

        _context.Transactions.Remove(transactionEntity);
        return await _context.SaveChangesAsync() == 1;
    }
}
