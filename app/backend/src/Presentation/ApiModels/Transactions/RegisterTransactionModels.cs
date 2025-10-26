namespace HouseholdBudget.Core.Presentation.ApiModels.Transactions;

/// <summary>
/// リクエストボディ
/// </summary>
public record RegisterTransactionRequest
{
    public required string UserId { get; set; }
    public required int Price { get; set; }
    public required string Type { get; set; }
    public string? Category { get; set; }
    public string? Memo { get; set; }
    public string? Place { get; set; }
}

/// <summary>
/// レスポンスボディ
/// </summary>
public record RegisterTransactionResponse
{
}
