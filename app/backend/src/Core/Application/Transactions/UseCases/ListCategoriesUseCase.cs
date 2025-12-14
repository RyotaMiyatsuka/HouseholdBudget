using HouseholdBudget.Core.Application.Common.Interfaces;
using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Application.Transactions.Interfaces;
using HouseholdBudget.Core.Application.Transactions.Results;
using HouseholdBudget.Core.Domain.Common.Interfaces;

namespace HouseholdBudget.Core.Application.Transactions.UseCases;

/// <summary>
/// カテゴリ一覧取得ユースケースの実装
/// </summary>
public class ListCategoriesUseCase : IListCategoriesUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public ListCategoriesUseCase(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<UseCaseResult<IEnumerable<CategoryResultData>>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        if (!_currentUserService.IsAuthenticated || _currentUserService.UserId == null)
        {
            return UseCaseResult<IEnumerable<CategoryResultData>>.Unauthorized();
        }

        var categories = await _unitOfWork.Categories.GetAllByUserIdAsync(
            _currentUserService.UserId.Value, cancellationToken);

        var results = categories.Select(c => new CategoryResultData(c.Id, c.Name));

        return UseCaseResult<IEnumerable<CategoryResultData>>.Success(results);
    }
}
