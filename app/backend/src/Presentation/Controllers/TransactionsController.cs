using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Application.Transactions.Commands;
using HouseholdBudget.Core.Application.Transactions.Interfaces;
using HouseholdBudget.Defines.Enums;
using HouseholdBudget.Presentation.DTOs.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace HouseholdBudget.Presentation.Controllers;

/// <summary>
/// 取引コントローラー
/// </summary>
[ApiController]
[Route("transactions")]
public class TransactionsController : ControllerBase
{
    private readonly IListTransactionsUseCase _listTransactionsUseCase;
    private readonly IGetTransactionsByMonthUseCase _getTransactionsByMonthUseCase;
    private readonly ICreateTransactionUseCase _createTransactionUseCase;
    private readonly IUpdateTransactionUseCase _updateTransactionUseCase;
    private readonly IDeleteTransactionUseCase _deleteTransactionUseCase;

    public TransactionsController(
        IListTransactionsUseCase listTransactionsUseCase,
        IGetTransactionsByMonthUseCase getTransactionsByMonthUseCase,
        ICreateTransactionUseCase createTransactionUseCase,
        IUpdateTransactionUseCase updateTransactionUseCase,
        IDeleteTransactionUseCase deleteTransactionUseCase)
    {
        _listTransactionsUseCase = listTransactionsUseCase;
        _getTransactionsByMonthUseCase = getTransactionsByMonthUseCase;
        _createTransactionUseCase = createTransactionUseCase;
        _updateTransactionUseCase = updateTransactionUseCase;
        _deleteTransactionUseCase = deleteTransactionUseCase;
    }

    /// <summary>
    /// 取引一覧取得
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TransactionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ListTransactions(CancellationToken cancellationToken)
    {
        var result = await _listTransactionsUseCase.ExecuteAsync(cancellationToken);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        var response = result.Data!.Select(MapToResponse);
        return Ok(response);
    }

    /// <summary>
    /// 月別取引情報取得
    /// </summary>
    [HttpGet("by-month")]
    [ProducesResponseType(typeof(IEnumerable<TransactionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTransactionsByMonth([FromQuery] int year, [FromQuery] int month, CancellationToken cancellationToken)
    {
        var command = new GetTransactionsByMonthCommand(year, month);
        var result = await _getTransactionsByMonthUseCase.ExecuteAsync(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        var response = result.Data!.Select(MapToResponse);
        return Ok(response);
    }

    /// <summary>
    /// 取引登録
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(TransactionResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionRequest request, CancellationToken cancellationToken)
    {
        if (!DateOnly.TryParse(request.Date, out var date))
        {
            return BadRequest("Invalid date format. Use YYYY-MM-DD.");
        }

        var command = new CreateTransactionCommand(
            request.Amount,
            request.Currency,
            date,
            MapToTransactionType(request.TransactionType),
            request.CategoryId,
            request.Memo,
            request.Place);

        var result = await _createTransactionUseCase.ExecuteAsync(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        var response = MapToResponse(result.Data!);
        return StatusCode(StatusCodes.Status201Created, response);
    }

    /// <summary>
    /// 取引更新
    /// </summary>
    [HttpPatch]
    [ProducesResponseType(typeof(TransactionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTransaction([FromBody] UpdateTransactionRequest request, CancellationToken cancellationToken)
    {
        if (!DateOnly.TryParse(request.Date, out var date))
        {
            return BadRequest("Invalid date format. Use YYYY-MM-DD.");
        }

        var command = new UpdateTransactionCommand(
            request.Id,
            request.Amount,
            request.Currency,
            date,
            MapToTransactionType(request.TransactionType),
            request.CategoryId,
            request.Memo,
            request.Place);

        var result = await _updateTransactionUseCase.ExecuteAsync(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        var response = MapToResponse(result.Data!);
        return Ok(response);
    }

    /// <summary>
    /// 取引削除
    /// </summary>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTransaction([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteTransactionCommand(id);
        var result = await _deleteTransactionUseCase.ExecuteAsync(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return NoContent();
    }

    /// <summary>
    /// 取引レポート取得 (未実装)
    /// </summary>
    [HttpGet("report")]
    public IActionResult GetTransactionReport()
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    /// <summary>
    /// 取引一括登録 (未実装)
    /// </summary>
    [HttpPost("import")]
    public IActionResult BulkCreateTransactions()
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    /// <summary>
    /// 取引情報エクスポート (未実装)
    /// </summary>
    [HttpGet("export")]
    public IActionResult ExportTransactions([FromQuery] string from, [FromQuery] string to)
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    /// <summary>
    /// 定期取引登録 (未実装)
    /// </summary>
    [HttpPost("recurring")]
    public IActionResult CreateRecurringTransaction()
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    /// <summary>
    /// 定期取引削除 (未実装)
    /// </summary>
    [HttpDelete("recurring")]
    public IActionResult DeleteRecurringTransaction()
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    /// <summary>
    /// 定期取引更新 (未実装)
    /// </summary>
    [HttpPatch("recurring")]
    public IActionResult UpdateRecurringTransaction()
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    private IActionResult HandleError<T>(UseCaseResult<T> result)
    {
        return result.ErrorType switch
        {
            UseCaseErrorType.Unauthorized => Unauthorized(result.ErrorMessage),
            UseCaseErrorType.NotFound => NotFound(result.ErrorMessage),
            UseCaseErrorType.Validation => BadRequest(result.ErrorMessage),
            _ => BadRequest(result.ErrorMessage)
        };
    }

    private IActionResult HandleError(UseCaseResult result)
    {
        return result.ErrorType switch
        {
            UseCaseErrorType.Unauthorized => Unauthorized(result.ErrorMessage),
            UseCaseErrorType.NotFound => NotFound(result.ErrorMessage),
            UseCaseErrorType.Validation => BadRequest(result.ErrorMessage),
            _ => BadRequest(result.ErrorMessage)
        };
    }

    private static TransactionResponse MapToResponse(Core.Application.Transactions.Results.TransactionResultData data)
    {
        return new TransactionResponse(
            data.Id,
            data.Amount,
            data.Currency,
            data.Date.ToString("yyyy-MM-dd"),
            data.TransactionType == TransactionType.Income ? TransactionTypeDto.Income : TransactionTypeDto.Expense,
            data.CategoryId,
            data.Memo,
            data.Place);
    }

    private static TransactionType MapToTransactionType(TransactionTypeDto dto)
    {
        return dto switch
        {
            TransactionTypeDto.Income => TransactionType.Income,
            TransactionTypeDto.Expense => TransactionType.Expense,
            _ => TransactionType.Expense
        };
    }
}
