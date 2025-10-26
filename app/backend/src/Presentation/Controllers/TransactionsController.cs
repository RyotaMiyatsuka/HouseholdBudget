using HouseholdBudget.Core.Application.Transactions.Dto;
using HouseholdBudget.Core.Application.Transactions.Interfaces;
using HouseholdBudget.Core.Presentation.ApiModels.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionUseCase _transactionUseCase;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="transactionUseCase"></param>
    public TransactionsController(ITransactionUseCase transactionUseCase)
    {
        _transactionUseCase = transactionUseCase;
    }

    // GET /api/transactions
    [HttpGet]
    public IActionResult GetTransactions([FromQuery] int? month, [FromQuery] int? year)
    {
        // Mock implementation
        var transactions = new List<Transaction>
        {
            new Transaction { Id = Guid.NewGuid().ToString(), Date = DateTime.UtcNow.ToString("yyyy-MM-dd"), Amount = 1000, Description = "Groceries", CategoryId = Guid.NewGuid().ToString() },
            new Transaction { Id = Guid.NewGuid().ToString(), Date = DateTime.UtcNow.ToString("yyyy-MM-dd"), Amount = 500, Description = "Coffee", CategoryId = Guid.NewGuid().ToString() }
        };
        return Ok(transactions);
    }

    // POST /api/transactions
    [HttpPost]
    public async Task<IActionResult> RegisterTransaction([FromBody] RegisterTransactionRequest request)
    {
        RegisterTransactionDto dto = new RegisterTransactionDto
        {
            UserId = request.UserId,
            Price = request.Price,
            Type = request.Type,
            Category = request.Category,
            Memo = request.Memo,
            Place = request.Place
        };
        await this._transactionUseCase.RegisterTransactionAsync(dto);

        // Mock implementation
        return StatusCode(201);
    }

    // PUT /api/transactions/{transactionId}
    [HttpPut("{transactionId}")]
    public IActionResult UpdateTransaction(string transactionId, [FromBody] UpdateTransaction updateTransaction)
    {
        // Mock implementation
        return Ok();
    }

    // POST /api/transactions/bulk
    [HttpPost("bulk")]
    public IActionResult CreateBulkTransactions([FromBody] IEnumerable<NewTransaction> newTransactions)
    {
        // Mock implementation
        return StatusCode(201); // Created
    }

    // GET /api/transactions/export
    [HttpGet("export")]
    public IActionResult ExportTransactions()
    {
        // Mock implementation
        var csvContent = "date,amount,description\n2025-09-15,1000,Groceries\n";
        return Content(csvContent, "text/csv");
    }

    // GET /api/transactions/summary
    [HttpGet("summary")]
    public IActionResult GetTransactionSummary()
    {
        // Mock implementation
        var summary = new { totalIncome = 5000, totalExpense = 3000, balance = 2000 };
        return Ok(summary);
    }
}

// --- DTOs based on openapi.yml ---

public class Transaction
{
    public string? Id { get; set; }
    public string? Date { get; set; }
    public decimal? Amount { get; set; }
    public string? Description { get; set; }
    public string? CategoryId { get; set; }
}

public class NewTransaction
{
    public string? Date { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public string? CategoryId { get; set; }
}

public class UpdateTransaction
{
    public string? Date { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public string? CategoryId { get; set; }
}
