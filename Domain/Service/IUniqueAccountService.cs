using Domain.Aggregate;
using Domain.Query;
namespace Domain.Service;

public interface IUniqueAccountService
{
    // For Login
    Task<UniqueAccountAggregate> GetUniqueAccountQueryHandlerAsync(GetUniqueAccountQuery query);

    // For CRUD
    Task<bool> CreateUniqueAccountAsync(UniqueAccountAggregate uniqueAccount);
    Task<List<UniqueAccountAggregate>> GetAllUniqueAccountsAsync();
    Task<UniqueAccountAggregate> GetUniqueAccountByIdAsync(string id);
    Task<bool> UpdateUniqueAccountAsync(UniqueAccountAggregate uniqueAccount);
    Task<bool> DeleteUniqueAccountAsync(string id);
}
