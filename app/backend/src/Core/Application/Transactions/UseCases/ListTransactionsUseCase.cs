using HouseholdBudget.Core.Application.Common.Interfaces;
using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Application.Transactions.Interfaces;
using HouseholdBudget.Core.Application.Transactions.Results;
using HouseholdBudget.Core.Domain.Common.Interfaces;

namespace HouseholdBudget.Core.Application.Transactions.UseCases;

/// <summary>
/// 取引一覧取得ユースケースの実装
/// </summary>
public class ListTransactionsUseCase : IListTransactionsUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public ListTransactionsUseCase(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<UseCaseResult<IEnumerable<TransactionResultData>>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        if (!_currentUserService.IsAuthenticated || _currentUserService.UserId == null)
        {
            return UseCaseResult<IEnumerable<TransactionResultData>>.Unauthorized();
        }

        var transactions = await _unitOfWork.Transactions.GetAllByUserIdAsync(
            _currentUserService.UserId.Value, cancellationToken);

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
