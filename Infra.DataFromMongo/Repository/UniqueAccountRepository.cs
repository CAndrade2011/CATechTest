using Domain.Aggregate;
using Domain.Repository;
using Infra.DataFromMongo.Entity;
using MongoDB.Driver;
using static MongoDB.Driver.WriteConcern;

namespace Infra.DataFromMongo.Repository;

public class UniqueAccountRepository : BaseContext, IUniqueAccountRepository, IDisposable
{
    private readonly IMongoCollection<UniqueAccount> _uniqueAccountsCollection;
    private const string COLLECTION_NAME = "unique-account";

    public UniqueAccountRepository(Microsoft.Extensions.Configuration.IConfiguration configuration) : base(configuration)
    {
        _uniqueAccountsCollection = base.GetCollection<UniqueAccount>(COLLECTION_NAME);

#if DEBUG
        InsertDefaultUser();
#endif
    }

    public async Task<bool> CreateUniqueAccountAsync(UniqueAccountAggregate UniqueAccount)
    {
        var entity = ToEntity(UniqueAccount);
        await _uniqueAccountsCollection.InsertOneAsync(entity);
        return SUCCESS_RETURN;
    }

    public async Task<List<UniqueAccountAggregate>> GetAllUniqueAccountsAsync(string? email, string? password)
    {
        var filters = Builders<UniqueAccount>.Filter.Empty;

        // Used for login
        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
        {
            filters &= Builders<UniqueAccount>.Filter.Eq(u => u.Email, email);
            filters &= Builders<UniqueAccount>.Filter.Eq(u => u.Password, BCrypt.Net.BCrypt.HashPassword(password));
        }

        return ToAggregate(await _uniqueAccountsCollection.Find(filters).ToListAsync());
    }

    public async Task<UniqueAccountAggregate> GetUniqueAccountByIdAsync(Guid id)
    {
        var filter = Builders<UniqueAccount>.Filter.Eq(p => p.Id, BaseContext.ToDbId(id));
        return ToAggregate(await _uniqueAccountsCollection.Find(filter).FirstOrDefaultAsync());
    }

    public async Task<bool> UpdateUniqueAccountAsync(UniqueAccountAggregate uniqueAccount)
    {
        var entity = ToEntity(uniqueAccount);
        var filter = Builders<UniqueAccount>.Filter.Eq(p => p.Id, entity.Id);
        var result = await _uniqueAccountsCollection.ReplaceOneAsync(filter, entity);
        return result.ModifiedCount >= 1 ? SUCCESS_RETURN : ERROR_RETURN;
    }

    public async Task<bool> DeleteUniqueAccountAsync(Guid id)
    {
        var filter = Builders<UniqueAccount>.Filter.Eq(p => p.Id, ToDbId(id));
        var result = await _uniqueAccountsCollection.DeleteOneAsync(filter);
        return result.DeletedCount >= 1 ? SUCCESS_RETURN : ERROR_RETURN;
    }

    private static UniqueAccount ToEntity(UniqueAccountAggregate? obj)
    {
        if (obj == null) return new();
        return new()
        {
            Id = BaseContext.ToDbId(obj.Id),
            DisplayName = obj.DisplayName,
            Email = string.IsNullOrWhiteSpace(obj.Email) ? string.Empty : obj.Email.ToLowerInvariant(),
            IsAdmin = obj.IsAdmin,
            LastRefreshToken = obj.LastRefreshToken,
            Name = obj.Name,
            Password = BCrypt.Net.BCrypt.HashPassword(obj.Password)
        };
    }

    private static UniqueAccountAggregate ToAggregate(UniqueAccount? obj)
    {
        if (obj == null) return new();
        return new()
        {
            Id = BaseContext.ToSystemId(obj.Id),
            DisplayName = obj.DisplayName,
            Email = obj.Email,
            IsAdmin = obj.IsAdmin,
            LastRefreshToken = obj.LastRefreshToken,
            Name = obj.Name
        };
    }

    private static List<UniqueAccountAggregate> ToAggregate(List<UniqueAccount>? obj)
    {
        if (obj?.Any() != true) return new();
        var ret = new List<UniqueAccountAggregate>();
        obj.ForEach(x => ret.Add(ToAggregate(x)));
        return ret;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

#if DEBUG
    private void InsertDefaultUser()
    {
        var defaultUser = GenerateDefaultUser();
        var entity = ToEntity(defaultUser);
        var filter = Builders<UniqueAccount>.Filter.Eq(p => p.Id, entity.Id);
        var result = _uniqueAccountsCollection.FindOneAndReplace(filter, entity);
    }

    private static UniqueAccountAggregate GenerateDefaultUser()
    {
        var defaultUserId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e");

        return new()
        {
            Id = defaultUserId,
            DisplayName = "Carlos",
            Email = "essenaotem@gmail.com",
            IsAdmin = true,
            LastRefreshToken = string.Empty,
            Name = "C. E. de Andrade",
            Password = BCrypt.Net.BCrypt.HashPassword("123456")
        };
    }

#endif
}
