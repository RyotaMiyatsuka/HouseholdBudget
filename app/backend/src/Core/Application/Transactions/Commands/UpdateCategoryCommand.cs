namespace HouseholdBudget.Core.Application.Transactions.Commands;

/// <summary>
/// カテゴリ更新コマンド
/// </summary>
public record UpdateCategoryCommand(string OldCategoryName, string NewCategoryName);
