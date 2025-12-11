using HouseholdBudget.Core.Application.Common.Interfaces;
using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Application.Transactions.Commands;
using HouseholdBudget.Core.Application.Transactions.Interfaces;
using HouseholdBudget.Core.Application.Transactions.Results;
using HouseholdBudget.Core.Domain.Common.Interfaces;
using HouseholdBudget.Core.Domain.Transactions.ValueObjects;

namespace HouseholdBudget.Core.Application.Transactions.UseCases;

/// <summary>
/// 取引更新ユースケースの実装
/// </summary>
public class UpdateTransactionUseCase : IUpdateTransactionUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public UpdateTransactionUseCase(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<UseCaseResult<TransactionResultData>> ExecuteAsync(UpdateTransactionCommand command, CancellationToken cancellationToken = default)
    {
        if (!_currentUserService.IsAuthenticated || _currentUserService.UserId == null)
        {
            return UseCaseResult<TransactionResultData>.Unauthorized();
        }

        var userId = _currentUserService.UserId.Value;

        // 取引の取得
        var transaction = await _unitOfWork.Transactions.GetByIdAndUserIdAsync(command.Id, userId, cancellationToken);
        if (transaction == null)
        {
            return UseCaseResult<TransactionResultData>.NotFound("Transaction not found.");
        }

        // カテゴリの存在確認
        var category = await _unitOfWork.Categories.GetByIdAndUserIdAsync(command.CategoryId, userId, cancellationToken);
        if (category == null)
        {
            return UseCaseResult<TransactionResultData>.NotFound("Category not found.");
        }

        // ValueObjectの作成
        Money money;
        try
        {
            money = Money.Create(command.Amount, command.Currency);
        }
        catch (Exception ex)
        {
            return UseCaseResult<TransactionResultData>.ValidationError(ex.Message);
        }

        var date = TransactionDate.Create(command.Date);

        // 取引の更新
        transaction.Update(
            money,
            date,
            command.TransactionType,
            command.CategoryId,
            command.Memo,
            command.Place);

        _unitOfWork.Transactions.Update(transaction);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return UseCaseResult<TransactionResultData>.Success(new TransactionResultData(
            transaction.Id,
            transaction.Money.Amount,
            transaction.Money.Currency,
            transaction.Date.Value,
            transaction.TransactionType,
            transaction.CategoryId,
            transaction.Memo,
            transaction.Place));
    }
}
