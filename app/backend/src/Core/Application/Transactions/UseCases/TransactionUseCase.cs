using HouseholdBudget.Core.Application.Transactions.Dto;
using HouseholdBudget.Core.Application.Transactions.Interfaces;
using HouseholdBudget.Core.Domain.Transactions.Entities;
using HouseholdBudget.Core.Domain.Transactions.Interfaces;
using HouseholdBudget.Core.Domain.Users.ValueObjects;

namespace HouseholdBudget.Core.Application.Transactions.UseCases;

public class TransactionUseCase : ITransactionUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="transactionRepository"></param>
    public TransactionUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task RegisterTransactionAsync(RegisterTransactionDto transactionCommand)
    {
        // ドメインモデル初期化
        Transaction transaction = new Transaction(
            userId: new LoginId(transactionCommand.UserId),
            price: transactionCommand.Price,
            type: transactionCommand.Type,
            csvImportId: null,
            recurringTransactionId: null,
            category: transactionCommand.Category,
            memo: transactionCommand.Memo,
            place: transactionCommand.Place
        );

        // 登録処理
        await this._unitOfWork.TransactionRepository.AddAsync(transaction);
        await this._unitOfWork.SaveChangesAsync();
    }
}
