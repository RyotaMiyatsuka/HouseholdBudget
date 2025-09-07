
using HouseholdBudget.Core.Domain.Users.Entities;

namespace HouseholdBudget.Core.Domain.Users.Interfaces;

public interface IUserService
{
    /// <summary>
    /// 存在確認
    /// </summary>
    /// <returns></returns>
    bool Exists(User user);
}