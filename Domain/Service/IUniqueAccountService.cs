using Domain.Aggregate;
using Domain.Query;
namespace Domain.Service;

public interface IUniqueAccountService
{
    Task<UniqueAccountAggregate> GetUniqueAccountQueryHandler(GetUniqueAccountQuery query);
}
