// using System.Reflection;
// using HouseholdBudget.Core.Domain.Users.Entities;
// using HouseholdBudget.Core.Domain.Users.Interfaces;
// using HouseholdBudget.Core.Domain.Users.ValueObjects;
// using HouseholdBudget.Infrastructure.EFCore;

// namespace HouseholdBudget.Infrastructure.Users;

// public class UserRepository : IUserRepository
// {
//     private readonly AppDbContext _context;

//     public UserRepository(AppDbContext context)
//     {
//         _context = context;
//     }

//     public void Add(User user)
//     {
//         var userEntity = ToPersistence(user);
//         _context.Users.Add(userEntity);
//         _context.SaveChanges();
//     }

//     public void Update(User user)
//     {
//         var userEntity = ToPersistence(user);
//         _context.Users.Update(userEntity);
//         _context.SaveChanges();
//     }

//     public User? FindByLoginId(LoginId loginId)
//     {
//         var userEntity = _context.Users.FirstOrDefault(u => u.LoginId == loginId.Value);
//         return userEntity is null ? null : ToDomain(userEntity);
//     }

//     public User? FindByName(UserName userName)
//     {
//         var userEntity = _context.Users.FirstOrDefault(u => u.UserName == userName.Value);
//         return userEntity is null ? null : ToDomain(userEntity);
//     }

//     public void Delete(User user)
//     {
//         var userEntity = ToPersistence(user);
//         _context.Users.Remove(userEntity);
//         _context.SaveChanges();
//     }

//     /// <summary>
//     /// ドメインモデルから永続化モデルへの変換
//     /// </summary>
//     private static EFCore.EFEntities.Users ToPersistence(User user)
//     {
//         return new EFCore.EFEntities.Users
//         {
//             Id = user.Id,
//             LoginId = user.LoginId.Value,
//             UserName = user.UserName.Value,
//             MailAddress = user.MailAddress,
//             IsVerifield = user.IsVerified
//         };
//     }

//     /// <summary>
//     /// 永続化モデルからドメインモデルへの変換
//     /// </summary>
//     private static User ToDomain(EFCore.EFEntities.Users userEntity)
//     {
//         var user = new User(userEntity.LoginId, userEntity.UserName, userEntity.MailAddress);

//         // リフレクションを使い、コンストラクタで自動生成された値をDBの値で上書きする
//         typeof(User).GetProperty(nameof(User.Id))!.SetValue(user, userEntity.Id);
//         typeof(User).GetProperty(nameof(User.IsVerified))!.SetValue(user, userEntity.IsVerifield);

//         return user;
//     }
// }
