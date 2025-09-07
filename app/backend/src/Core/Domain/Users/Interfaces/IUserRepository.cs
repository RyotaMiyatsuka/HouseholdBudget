using HouseholdBudget.Core.Domain.Users.Entities;
using HouseholdBudget.Core.Domain.Users.ValueObjects;

namespace HouseholdBudget.Core.Domain.Users.Interfaces;

public interface IUserRepository
{
    /// <summary>
    /// 保存
    /// </summary>
    void Add(User user);

    /// <summary>
    /// 更新
    /// </summary>
    void Update(User user);

    /// <summary>
    /// LoginId検索
    /// </summary>
    /// <returns></returns>
    User? FindByLoginId(LoginId loginId);

    /// <summary>
    /// 名前検索
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    User? FindByName(UserName userName);

    /// <summary>
    /// 削除
    /// </summary>
    void Delete(User user);
}