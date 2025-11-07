using HouseholdBudget.Core.Application.Transactions.Dto;
using HouseholdBudget.Core.Application.Transactions.Interfaces;
using HouseholdBudget.Core.Domain.Transactions.Entities;
using HouseholdBudget.Core.Domain.Transactions.Interfaces;
using HouseholdBudget.Core.Domain.Transactions.ValueObjects;
using HouseholdBudget.Core.Domain.Users.ValueObjects;
using HouseholdBudget.Defines;

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
    public async Task RegisterTransactionAsync(RegisterTransactionDto dto)
    {
        // TODO: エラーメッセージ共通化
        // Enum をパース
        if (!Enum.TryParse<Currency>(dto.Currency, out var currency))
        {
            string invalidFieldMessage = "フィールド {0} の値が不正です。";
            throw new ArgumentException(string.Format(invalidFieldMessage, nameof(dto.Currency)));
        }

        // ドメインモデル初期化
        Transaction transaction = new Transaction(
            userId: new LoginId(dto.UserId),
            price: new Price(dto.Amount, currency),
            transactionType: dto.TransactionType,
            csvImportId: null,
            recurringTransactionId: null,
            categoryId: dto.CategoryId,
            memo: dto.Memo,
            place: dto.Place
        );

        // 登録処理
        await this._unitOfWork.TransactionRepository.AddAsync(transaction);
        await this._unitOfWork.SaveChangesAsync();
    }
}
