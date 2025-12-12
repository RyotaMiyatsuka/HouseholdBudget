using HouseholdBudget.Core.Application.Common.Interfaces;
using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Application.Transactions.Commands;
using HouseholdBudget.Core.Application.Transactions.Interfaces;
using HouseholdBudget.Core.Application.Transactions.Results;
using HouseholdBudget.Core.Domain.Common.Interfaces;
using HouseholdBudget.Core.Domain.Transactions.Entities;
using HouseholdBudget.Core.Domain.Transactions.ValueObjects;

namespace HouseholdBudget.Core.Application.Transactions.UseCases;

/// <summary>
/// 取引登録ユースケースの実装
/// </summary>
public class CreateTransactionUseCase : ICreateTransactionUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public CreateTransactionUseCase(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<UseCaseResult<TransactionResultData>> ExecuteAsync(CreateTransactionCommand command, CancellationToken cancellationToken = default)
    {
        if (!_currentUserService.IsAuthenticated || _currentUserService.UserId == null)
        {
            return UseCaseResult<TransactionResultData>.Unauthorized();
        }

        var userId = _currentUserService.UserId.Value;

        // カテゴリの存在確認（名前で検索）
        var category = await _unitOfWork.Categories.GetByNameAndUserIdAsync(command.CategoryName, userId, cancellationToken);
        if (category == null)
        {
            return UseCaseResult<TransactionResultData>.NotFound($"Category '{command.CategoryName}' not found.");
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

        // 取引の作成
        var transaction = Transaction.Create(
            money,
            date,
            command.TransactionType,
            category.Id,
            userId,
            command.Memo,
            command.Place);

        await _unitOfWork.Transactions.AddAsync(transaction, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return UseCaseResult<TransactionResultData>.Success(new TransactionResultData(
            transaction.Id,
            transaction.Money.Amount,
            transaction.Money.Currency,
            transaction.Date.Value,
            transaction.TransactionType,
            category.Name,
            transaction.Memo,
            transaction.Place));
    }
}
