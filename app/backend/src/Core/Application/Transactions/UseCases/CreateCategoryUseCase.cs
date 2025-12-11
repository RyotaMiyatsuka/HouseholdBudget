using HouseholdBudget.Core.Application.Common.Interfaces;
using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Application.Transactions.Commands;
using HouseholdBudget.Core.Application.Transactions.Interfaces;
using HouseholdBudget.Core.Application.Transactions.Results;
using HouseholdBudget.Core.Domain.Common.Interfaces;
using HouseholdBudget.Core.Domain.Transactions.Entities;

namespace HouseholdBudget.Core.Application.Transactions.UseCases;

/// <summary>
/// カテゴリ登録ユースケースの実装
/// </summary>
public class CreateCategoryUseCase : ICreateCategoryUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public CreateCategoryUseCase(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<UseCaseResult<CategoryResultData>> ExecuteAsync(CreateCategoryCommand command, CancellationToken cancellationToken = default)
    {
        if (!_currentUserService.IsAuthenticated || _currentUserService.UserId == null)
        {
            return UseCaseResult<CategoryResultData>.Unauthorized();
        }

        var userId = _currentUserService.UserId.Value;

        // 同名カテゴリの存在確認
        if (await _unitOfWork.Categories.ExistsByNameAndUserIdAsync(command.CategoryName, userId, cancellationToken))
        {
            return UseCaseResult<CategoryResultData>.Conflict("Category with this name already exists.");
        }

        // カテゴリの作成
        Category category;
        try
        {
            category = Category.Create(command.CategoryName, userId);
        }
        catch (Exception ex)
        {
            return UseCaseResult<CategoryResultData>.ValidationError(ex.Message);
        }

        await _unitOfWork.Categories.AddAsync(category, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return UseCaseResult<CategoryResultData>.Success(new CategoryResultData(category.Id, category.Name));
    }
}
