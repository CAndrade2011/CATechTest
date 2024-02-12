using Domain.Aggregate;

namespace Domain.Repository;

public interface IUniqueAccountRepository
{
    Task<bool> CreateUniqueAccountAsync(UniqueAccountAggregate uniqueAccount);
    Task<List<UniqueAccountAggregate>> GetAllUniqueAccountsAsync(string? email, string? password);
    Task<UniqueAccountAggregate> GetUniqueAccountByIdAsync(Guid id);
    Task<bool> UpdateUniqueAccountAsync(UniqueAccountAggregate uniqueAccount);
    Task<bool> DeleteUniqueAccountAsync(Guid id);
}
