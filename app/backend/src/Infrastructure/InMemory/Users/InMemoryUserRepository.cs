using HouseholdBudget.Core.Domain.Users.Entities;
using HouseholdBudget.Core.Domain.Users.Interfaces;
using HouseholdBudget.Core.Domain.Users.ValueObjects;

namespace HouseholdBudget.Infrastructure.InMemory.Users;

/// <summary>
/// メモリ上に保持するユーザーリポジトリ. 動作確認用.
/// </summary>
public class InMemoryUserRepository : IUserRepository
{
    /// <summary>
    /// ユーザーデータをメモリ上に保存するためのDictionary
    /// キーはユーザーID、値はUserエンティティ
    /// </summary>
    private readonly Dictionary<string, User> _users = [];

    /// <summary>
    /// 保存
    /// </summary>
    public void Add(User user)
    {
        // ユーザーIDが既に存在するか確認
        if (!_users.TryAdd(user.Id, user))
        {
            // 既存のユーザー情報を更新
            _users[user.Id] = user;
        }
    }

    /// <summary>
    /// LoginId検索
    /// </summary>
    /// <returns></returns>
    public User? FindByLoginId(LoginId loginId)
    {
        // ユーザー名の値を文字列として取得
        string loginIdValue = loginId.Value;

        // Dictionaryの値をループして、名前が一致するユーザーを検索
        // SingleOrDefaultを使うと、複数のユーザーが見つかった場合に例外が発生するため注意
        return _users.Values.SingleOrDefault(u => u.LoginId.Value == loginIdValue);
    }

    /// <summary>
    /// 名前検索
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public User? FindByName(UserName userName)
    {
        // ユーザー名の値を文字列として取得
        string nameValue = userName.Value;

        // Dictionaryの値をループして、名前が一致するユーザーを検索
        // SingleOrDefaultを使うと、複数のユーザーが見つかった場合に例外が発生するため注意
        return _users.Values.SingleOrDefault(u => u.UserName.Value == nameValue);
    }

    /// <summary>
    /// 削除
    /// </summary>
    public void Delete(User user)
    {
        // 指定されたユーザーをDictionaryから削除
        _users.Remove(user.Id);
    }
}