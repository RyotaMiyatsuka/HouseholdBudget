using HouseholdBudget.Core.Application.Auth.Commands;
using HouseholdBudget.Core.Application.Auth.Interfaces;
using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Domain.Common.Interfaces;

namespace HouseholdBudget.Core.Application.Auth.UseCases;

/// <summary>
/// ログアウトユースケースの実装
/// </summary>
public class LogoutUseCase : ILogoutUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public LogoutUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UseCaseResult> ExecuteAsync(LogoutCommand command, CancellationToken cancellationToken = default)
    {
        var session = await _unitOfWork.Sessions.GetByIdAsync(command.SessionId, cancellationToken);

        if (session == null || !session.IsValid())
        {
            return UseCaseResult.NotFound("Session not found or already expired.");
        }

        session.Invalidate();
        _unitOfWork.Sessions.Update(session);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return UseCaseResult.Success();
    }
}
