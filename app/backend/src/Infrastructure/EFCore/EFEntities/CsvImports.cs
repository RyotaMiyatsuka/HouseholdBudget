
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseholdBudget.Infrastructure.EFCore.EFEntities;

public class CsvImports
{
    public string Id { get; set; }

    /// <summary>
    /// 取り込んだユーザー
    /// </summary>
    [Required]
    [ForeignKey("User")]
    public string UserId { get; set; }

    public virtual Users User { get; set; }
}
