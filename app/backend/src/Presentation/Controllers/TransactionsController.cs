using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
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
    public IActionResult CreateTransaction([FromBody] NewTransaction newTransaction)
    {
        // Mock implementation
        return StatusCode(201); // Created
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
    public string Id { get; set; }
    public string Date { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public string CategoryId { get; set; }
}

public class NewTransaction
{
    public string Date { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public string CategoryId { get; set; }
}

public class UpdateTransaction
{
    public string Date { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public string CategoryId { get; set; }
}
