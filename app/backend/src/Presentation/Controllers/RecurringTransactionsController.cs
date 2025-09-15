using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/recurring-transactions")]
public class RecurringTransactionsController : ControllerBase
{
    // POST /api/recurring-transactions
    [HttpPost]
    public IActionResult CreateRecurringTransaction()
    {
        // Mock implementation
        return StatusCode(201); // Created
    }
}
