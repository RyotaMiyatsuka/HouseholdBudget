
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseholdBudget.Infrastructure.EFCore.EFEntities;

public class RecurringTransactions
{
    public string Id { get; set; }

    /// <summary>
    /// 登録したユーザー
    /// </summary>
    [Required]
    [ForeignKey("User")]
    public string UserId { get; set; }

    public virtual Users User { get; set; }
}
