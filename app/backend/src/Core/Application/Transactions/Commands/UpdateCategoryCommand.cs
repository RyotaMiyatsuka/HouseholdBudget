namespace HouseholdBudget.Core.Application.Transactions.Commands;

/// <summary>
/// カテゴリ更新コマンド
/// </summary>
public record UpdateCategoryCommand(Guid CategoryId, string CategoryName);
