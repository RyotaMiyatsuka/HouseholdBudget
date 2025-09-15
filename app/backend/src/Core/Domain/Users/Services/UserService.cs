using HouseholdBudget.Core.Domain.Users.Entities;
using HouseholdBudget.Core.Domain.Users.Interfaces;

namespace HouseholdBudget.Core.Domain.Users.Services;

public class UserService : IUserService
{
    private IUserRepository userRepository;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    /// <summary>
    /// 存在確認
    /// </summary>
    /// <returns></returns>
    public bool Exists(User user)
    {
        return this.userRepository.FindByName(userName: user.UserName) != null;
    }
}