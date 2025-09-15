using HouseholdBudget.Core.Application.Users.Interfaces;
using HouseholdBudget.Core.Domain.Users.Entities;
using HouseholdBudget.Core.Domain.Users.Interfaces;
using HouseholdBudget.Core.Domain.Users.ValueObjects;
using HouseholdBudget.Defines;
using HouseholdBudget.Core.Application.Users.Commands;

namespace HouseholdBudget.Core.Application.Users.UseCases;

public class UserUseCase : IUserUseCase
{
    private readonly IUserRepository userRepository;
    private readonly IUserService userService;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="userRepository"></param>
    /// <param name="userService"></param>
    public UserUseCase(IUserRepository userRepository, IUserService userService)
    {
        this.userRepository = userRepository;
        this.userService = userService;
    }

    /// <summary>
    /// ユーザー登録
    /// </summary>
    /// <param name="loginId"></param>
    /// <param name="userName"></param>
    /// <param name="mailAddress"></param>
    /// <exception cref="UserExistsException"></exception>
    public void Register(string loginId, string userName, string mailAddress)
    {
        User user = new(loginId, userName, mailAddress);

        // 存在確認
        if (this.userService.Exists(user))
        {
            // TODO: エラーメッセージ共通化
            string mes = "ユーザーは既に存在しています。";
            throw new UserExistsException(mes);
        }

        // 登録
        this.userRepository.Add(user);
    }

    /// <summary>
    /// ユーザー取得処理
    /// </summary>
    /// <param name="loginId"></param>
    public User Get(string loginId)
    {
        // 取得
        User? user = this.userRepository.FindByLoginId(new LoginId(loginId));

        // nullチェック
        if (user == null)
        {
            // TODO: エラーメッセージ共通化
            string mes = "指定したユーザーが存在しません。";
            throw new UserNotFoundException(mes);
        }

        return user;
    }

    /// <summary>
    /// ユーザー編集処理
    /// </summary>
    public void Edit(UserEditCommand command)
    {
        // ユーザー取得
        User user = this.Get(command.LoginId);

        // 指定された項目を編集
        // LoginId
        if (!string.IsNullOrEmpty(command.NewLoginId))
        {
            user.ChangeLoginId(command.NewLoginId);
        }
        // UserName
        if (!string.IsNullOrEmpty(command.NewUserName))
        {
            user.ChangeUserName(command.NewUserName);
        }
        // MailAddress
        if (!string.IsNullOrEmpty(command.NewMailAddress))
        {
            user.ChangeMailAddress(command.NewMailAddress);
        }

        // 更新
        this.userRepository.Update(user);
    }

    /// <summary>
    /// ユーザー削除処理
    /// </summary>
    public void Delete(string loginId, string mailAddress)
    {
        // 取得
        User user = this.Get(loginId);
        // 削除処理
        this.userRepository.Delete(user);
    }
}