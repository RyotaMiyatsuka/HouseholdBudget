using HouseholdBudget.Core.Application.Common.Interfaces;
using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Application.Transactions.Commands;
using HouseholdBudget.Core.Application.Transactions.Interfaces;
using HouseholdBudget.Core.Domain.Common.Interfaces;

namespace HouseholdBudget.Core.Application.Transactions.UseCases;

/// <summary>
/// 取引削除ユースケースの実装
/// </summary>
public class DeleteTransactionUseCase : IDeleteTransactionUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public DeleteTransactionUseCase(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<UseCaseResult> ExecuteAsync(DeleteTransactionCommand command, CancellationToken cancellationToken = default)
    {
        if (!_currentUserService.IsAuthenticated || _currentUserService.UserId == null)
        {
            return UseCaseResult.Unauthorized();
        }

        var userId = _currentUserService.UserId.Value;

        // 取引の取得
        var transaction = await _unitOfWork.Transactions.GetByIdAndUserIdAsync(command.Id, userId, cancellationToken);
        if (transaction == null)
        {
            return UseCaseResult.NotFound("Transaction not found.");
        }

        // 取引の削除
        _unitOfWork.Transactions.Delete(transaction);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return UseCaseResult.Success();
    }
}
