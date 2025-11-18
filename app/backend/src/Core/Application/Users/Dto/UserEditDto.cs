namespace HouseholdBudget.Core.Application.Users.Commands;

public class UserEditDto
{
    public required string LoginId { get; set; }
    public string? NewLoginId { get; set; }
    public string? NewUserName { get; set; }
    public string? NewMailAddress { get; set; }
}
