using HouseholdBudget.Core.Application.Common.Interfaces;
using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Application.Transactions.Commands;
using HouseholdBudget.Core.Application.Transactions.Interfaces;
using HouseholdBudget.Core.Application.Transactions.Results;
using HouseholdBudget.Core.Domain.Common.Interfaces;

namespace HouseholdBudget.Core.Application.Transactions.UseCases;

/// <summary>
/// カテゴリ更新ユースケースの実装
/// </summary>
public class UpdateCategoryUseCase : IUpdateCategoryUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public UpdateCategoryUseCase(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<UseCaseResult<CategoryResultData>> ExecuteAsync(UpdateCategoryCommand command, CancellationToken cancellationToken = default)
    {
        if (!_currentUserService.IsAuthenticated || _currentUserService.UserId == null)
        {
            return UseCaseResult<CategoryResultData>.Unauthorized();
        }

        var userId = _currentUserService.UserId.Value;

        // カテゴリの取得
        var category = await _unitOfWork.Categories.GetByIdAndUserIdAsync(command.CategoryId, userId, cancellationToken);
        if (category == null)
        {
            return UseCaseResult<CategoryResultData>.NotFound("Category not found.");
        }

        // 同名カテゴリの存在確認（自分自身は除く）
        if (category.Name != command.CategoryName &&
            await _unitOfWork.Categories.ExistsByNameAndUserIdAsync(command.CategoryName, userId, cancellationToken))
        {
            return UseCaseResult<CategoryResultData>.Conflict("Category with this name already exists.");
        }

        // カテゴリの更新
        try
        {
            category.UpdateName(command.CategoryName);
        }
        catch (Exception ex)
        {
            return UseCaseResult<CategoryResultData>.ValidationError(ex.Message);
        }

        _unitOfWork.Categories.Update(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return UseCaseResult<CategoryResultData>.Success(new CategoryResultData(category.Id, category.Name));
    }
}
