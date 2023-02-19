using Microsoft.AspNetCore.Mvc;

namespace SalesTracker.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            return Ok(transactions);
        }

        [HttpGet]
        [Route("{transactionId}")]
        public async Task<IActionResult> GetTransactionById([FromRoute] int transactionId)
        {
            var detail = await _transactionService.GetTransactionByIdAsync(transactionId);
            return detail is not null ? Ok(detail) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransactionAsync([FromBody] TransactionCreate transactionToCreate)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _transactionService.CreateTransactionAsync(transactionToCreate));
        }

        [HttpPut]
        public async Task<IActionResult> TransactionUpdate([FromBody] TransactionEdit request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _transactionService.UpdateTransactionAsync(request)
            ? Ok("Transaction updated successfully.")
            : BadRequest("Transaction could not be updated.");
        }

        [HttpDelete]
        [Route("{transactionId}")]
        public async Task<IActionResult> DeleteTransaction([FromRoute] int transactionId)
        {
            return await _transactionService.DeleteTransactionAsync(transactionId)
            ? Ok($"Transaction {transactionId} was deleted successfully.")
            : BadRequest($"Transaction {transactionId} could not be deleted.");
        }
    }
}