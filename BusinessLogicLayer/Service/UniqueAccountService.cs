using Domain.Query;
using Domain.Aggregate;
using Domain.Repository;
using Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infra.DataFromMongo.Entity;

namespace BusinessLogicLayer.Service;

public class UniqueAccountService : IUniqueAccountService
{
    private readonly IUniqueAccountRepository _uniqueAccountRepository;

    public UniqueAccountService(IUniqueAccountRepository uniqueAccountRepository)
    {
        _uniqueAccountRepository = uniqueAccountRepository;
    }

    public async Task<UniqueAccountAggregate> GetUniqueAccountQueryHandlerAsync(GetUniqueAccountQuery query)
    {
        ArgumentNullException.ThrowIfNull(query, nameof(query));
        ArgumentNullException.ThrowIfNullOrWhiteSpace(query.Email, nameof(query.Email));
        ArgumentNullException.ThrowIfNullOrWhiteSpace(query.Password, nameof(query.Password));
        var list = await _uniqueAccountRepository.GetAllUniqueAccountsAsync(query.Email, query.Password);
        return list!.FirstOrDefault() ?? new UniqueAccountAggregate();
    }

    public async Task<bool> CreateUniqueAccountAsync(UniqueAccountAggregate uniqueAccount)
    {
        return await _uniqueAccountRepository.CreateUniqueAccountAsync(uniqueAccount);
    }

    public async Task<UniqueAccountAggregate> GetUniqueAccountByIdAsync(string id)
    {
        return await _uniqueAccountRepository.GetUniqueAccountByIdAsync(id);
    }

    public async Task<bool> UpdateUniqueAccountAsync(UniqueAccountAggregate uniqueAccount)
    {
        return await _uniqueAccountRepository.UpdateUniqueAccountAsync(uniqueAccount);
    }

    public async Task<bool> DeleteUniqueAccountAsync(string id)
    {
        return await _uniqueAccountRepository.DeleteUniqueAccountAsync(id);
    }

    public async Task<List<UniqueAccountAggregate>> GetAllUniqueAccountsAsync()
    {
        return await _uniqueAccountRepository.GetAllUniqueAccountsAsync(null, null);
    }
}

