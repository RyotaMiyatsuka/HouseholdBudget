using HouseholdBudget.Core.Application.Transactions.Dto;
namespace HouseholdBudget.Core.Application.Transactions.Interfaces;

/// <summary>
/// 取引ユースケースインターフェース
/// </summary>
public interface ITransactionUseCase
{
    /// <summary>
    /// 取引の登録
    /// </summary>
    Task RegisterTransactionAsync(RegisterTransactionDto transactionCommand);
}

