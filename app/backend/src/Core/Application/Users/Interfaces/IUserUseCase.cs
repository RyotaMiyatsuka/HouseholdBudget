using HouseholdBudget.Core.Domain.Users.Entities;
using HouseholdBudget.Core.Application.Users.Commands;

namespace HouseholdBudget.Core.Application.Users.Interfaces;

public interface IUserUseCase
{
    /// <summary>
    /// ユーザー登録処理
    /// </summary>
    void Register(string loginId, string userName, string mailAddress);

    /// <summary>
    /// ユーザー取得処理
    /// </summary>
    /// <param name="loginId"></param>
    User Get(string loginId);

    /// <summary>
    /// ユーザー編集処理
    /// </summary>
    void Edit(UserEditCommand command);

    /// <summary>
    /// ユーザー削除処理
    /// </summary>
    void Delete(string loginId, string mailAddress);
}