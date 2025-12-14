using HouseholdBudget.Core.Domain.Common;
using HouseholdBudget.Core.Domain.Transactions.ValueObjects;
using HouseholdBudget.Defines.Enums;

namespace HouseholdBudget.Core.Domain.Transactions.Entities;

/// <summary>
/// 取引エンティティ
/// </summary>
public class Transaction : Entity<Guid>
{
    public const int MaxMemoLength = 500;
    public const int MaxPlaceLength = 100;

    public Money Money { get; private set; } = null!;
    public TransactionDate Date { get; private set; } = null!;
    public TransactionType TransactionType { get; private set; }
    public Guid CategoryId { get; private set; }
    public string? Memo { get; private set; }
    public string? Place { get; private set; }
    public Guid UserId { get; private set; }

    private Transaction() : base()
    {
    }

    private Transaction(
        Guid id,
        Money money,
        TransactionDate date,
        TransactionType transactionType,
        Guid categoryId,
        string? memo,
        string? place,
        Guid userId) : base(id)
    {
        Money = money;
        Date = date;
        TransactionType = transactionType;
        CategoryId = categoryId;
        Memo = memo;
        Place = place;
        UserId = userId;
    }

    public static Transaction Create(
        Money money,
        TransactionDate date,
        TransactionType transactionType,
        Guid categoryId,
        Guid userId,
        string? memo = null,
        string? place = null)
    {
        ValidateMemo(memo);
        ValidatePlace(place);

        return new Transaction(
            Guid.NewGuid(),
            money,
            date,
            transactionType,
            categoryId,
            memo?.Trim(),
            place?.Trim(),
            userId);
    }

    public void Update(
        Money money,
        TransactionDate date,
        TransactionType transactionType,
        Guid categoryId,
        string? memo,
        string? place)
    {
        ValidateMemo(memo);
        ValidatePlace(place);

        Money = money;
        Date = date;
        TransactionType = transactionType;
        CategoryId = categoryId;
        Memo = memo?.Trim();
        Place = place?.Trim();
        SetUpdatedAt();
    }

    private static void ValidateMemo(string? memo)
    {
        if (memo != null && memo.Length > MaxMemoLength)
            throw new Defines.Exceptions.ValidationException($"Memo must not exceed {MaxMemoLength} characters.");
    }

    private static void ValidatePlace(string? place)
    {
        if (place != null && place.Length > MaxPlaceLength)
            throw new Defines.Exceptions.ValidationException($"Place must not exceed {MaxPlaceLength} characters.");
    }
}
