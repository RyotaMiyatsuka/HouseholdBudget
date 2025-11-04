namespace HouseholdBudget.Core.Presentation.ApiModels.Transactions;

/// <summary>
/// リクエストボディ
/// </summary>
public record RegisterTransactionRequest
{
    public required decimal Amount { get; set; }
    public required string Currency { get; set; }
    public required string Date { get; set; }
    public required string TransactionType { get; set; }
    public required string CategoryId { get; set; }
    public string Memo { get; set; } = "";
    public string Place { get; set; } = "";
}

/// <summary>
/// レスポンスボディ
/// </summary>
public record RegisterTransactionResponse
{
}
