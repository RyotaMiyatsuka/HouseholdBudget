using HouseholdBudget.Core.Application.Common.Interfaces;
using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Application.Transactions.Commands;
using HouseholdBudget.Core.Application.Transactions.Interfaces;
using HouseholdBudget.Core.Application.Transactions.Results;
using HouseholdBudget.Core.Domain.Common.Interfaces;

namespace HouseholdBudget.Core.Application.Transactions.UseCases;

/// <summary>
/// 月別取引取得ユースケースの実装
/// </summary>
public class GetTransactionsByMonthUseCase : IGetTransactionsByMonthUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public GetTransactionsByMonthUseCase(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<UseCaseResult<IEnumerable<TransactionResultData>>> ExecuteAsync(GetTransactionsByMonthCommand command, CancellationToken cancellationToken = default)
    {
        if (!_currentUserService.IsAuthenticated || _currentUserService.UserId == null)
        {
            return UseCaseResult<IEnumerable<TransactionResultData>>.Unauthorized();
        }

        if (command.Month < 1 || command.Month > 12)
        {
            return UseCaseResult<IEnumerable<TransactionResultData>>.ValidationError("Month must be between 1 and 12.");
        }

        if (command.Year < 1900 || command.Year > 9999)
        {
            return UseCaseResult<IEnumerable<TransactionResultData>>.ValidationError("Invalid year.");
        }

        var transactions = await _unitOfWork.Transactions.GetByUserIdAndMonthAsync(
            _currentUserService.UserId.Value, command.Year, command.Month, cancellationToken);

        var results = transactions.Select(t => new TransactionResultData(
            t.Id,
            t.Money.Amount,
            t.Money.Currency,
            t.Date.Value,
            t.TransactionType,
            t.CategoryId,
            t.Memo,
            t.Place));

        return UseCaseResult<IEnumerable<TransactionResultData>>.Success(results);
    }
}
