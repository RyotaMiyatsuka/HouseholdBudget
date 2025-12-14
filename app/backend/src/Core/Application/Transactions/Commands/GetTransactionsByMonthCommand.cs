namespace HouseholdBudget.Core.Application.Transactions.Commands;

/// <summary>
/// 月別取引取得コマンド
/// </summary>
public record GetTransactionsByMonthCommand(int Year, int Month);
