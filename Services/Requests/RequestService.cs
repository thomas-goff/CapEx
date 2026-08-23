using CapEx.Models;
using CapEx.Repositories;
using CapEx.Services.Approvals;

namespace CapEx.Services.Requests;

public sealed class RequestService : IRequestService
{
    private readonly IRequestRepository _requests;
    private readonly IApprovalTierPolicy _tierPolicy;

    public RequestService(IRequestRepository requests, IApprovalTierPolicy tierPolicy)
    {
        _requests = requests;
        _tierPolicy = tierPolicy;
    }

    public Task<IReadOnlyList<Request>> GetAllAsync(CancellationToken cancellationToken = default)
        => _requests.GetAllAsync(cancellationToken);

    public Task<Request?> GetByIdAsync(int requestId, CancellationToken cancellationToken = default)
        => _requests.GetByIdAsync(requestId, cancellationToken);

    public async Task<Request> CreateAsync(
        CreateRequestCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (string.IsNullOrWhiteSpace(command.Title))
        {
            throw new ArgumentException("Title is required.", nameof(command));
        }

        if (string.IsNullOrWhiteSpace(command.Motivation))
        {
            throw new ArgumentException("Motivation is required.", nameof(command));
        }

        if (command.Amount <= 0m)
        {
            throw new ArgumentOutOfRangeException(nameof(command), "Amount must be above zero.");
        }

        var requiredApprovers = _tierPolicy.GetRequiredApprovers(command.Amount);

        var request = new Request
        {
            RequestedByUserId = command.RequestedByUserId,
            Title = command.Title.Trim(),
            Amount = command.Amount,
            Motivation = command.Motivation.Trim(),

            Status = requiredApprovers.Count == 0
                ? RequestStatus.Approved
                : RequestStatus.Pending
        };

        return await _requests.AddAsync(request, cancellationToken);
    }
}
