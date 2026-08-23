using CapEx.Models;

namespace CapEx.Services.Requests;

public interface IRequestService
{
    Task<IReadOnlyList<Request>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Request?> GetByIdAsync(int requestId, CancellationToken cancellationToken = default);

    Task<Request> CreateAsync(CreateRequestCommand command, CancellationToken cancellationToken = default);
}
