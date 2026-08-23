using CapEx.Models;

namespace CapEx.Repositories;

public interface IRequestRepository
{
    Task<IReadOnlyList<Request>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Request?> GetByIdAsync(int requestId, CancellationToken cancellationToken = default);

    Task<Request> AddAsync(Request request, CancellationToken cancellationToken = default);
}
