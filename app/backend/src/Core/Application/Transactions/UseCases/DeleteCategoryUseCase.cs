using HouseholdBudget.Core.Application.Common.Interfaces;
using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Application.Transactions.Commands;
using HouseholdBudget.Core.Application.Transactions.Interfaces;
using HouseholdBudget.Core.Domain.Common.Interfaces;

namespace HouseholdBudget.Core.Application.Transactions.UseCases;

/// <summary>
/// カテゴリ削除ユースケースの実装
/// </summary>
public class DeleteCategoryUseCase : IDeleteCategoryUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public DeleteCategoryUseCase(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<UseCaseResult> ExecuteAsync(DeleteCategoryCommand command, CancellationToken cancellationToken = default)
    {
        if (!_currentUserService.IsAuthenticated || _currentUserService.UserId == null)
        {
            return UseCaseResult.Unauthorized();
        }

        var userId = _currentUserService.UserId.Value;

        // カテゴリの取得
        var category = await _unitOfWork.Categories.GetByIdAndUserIdAsync(command.Id, userId, cancellationToken);
        if (category == null)
        {
            return UseCaseResult.NotFound("Category not found.");
        }

        // カテゴリの削除
        _unitOfWork.Categories.Delete(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return UseCaseResult.Success();
    }
}
