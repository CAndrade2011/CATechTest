using Domain.Query;
using Domain.Aggregate;
using Domain.Repository;
using Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service;

public class UniqueAccountService : IUniqueAccountService
{
    private readonly IUniqueAccountRepository _uniqueAccountRepository;

    public UniqueAccountService(IUniqueAccountRepository uniqueAccountRepository)
    {
        _uniqueAccountRepository = uniqueAccountRepository;
    }

    public async Task<UniqueAccountAggregate> GetUniqueAccountQueryHandler(GetUniqueAccountQuery query)
    {
        ArgumentNullException.ThrowIfNull(query, nameof(query));
        ArgumentNullException.ThrowIfNullOrWhiteSpace(query.Email, nameof(query.Email));
        ArgumentNullException.ThrowIfNullOrWhiteSpace(query.Password, nameof(query.Password));
        var list = await _uniqueAccountRepository.GetAllUniqueAccountsAsync(query.Email, query.Password);
        return list!.FirstOrDefault() ?? new UniqueAccountAggregate();
    }
}
