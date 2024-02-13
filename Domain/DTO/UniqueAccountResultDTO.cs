using Domain.Aggregate;

namespace Domain.DTO;

public class UniqueAccountResultDTO
{
    public UniqueAccountResultDTO(UniqueAccountAggregate uniqueAccount)
    {
        this.Id = uniqueAccount.Id;
        this.Name = uniqueAccount.Name;
        this.DisplayName = uniqueAccount.DisplayName;
        this.Email = uniqueAccount.Email;
        this.LastRefreshToken = uniqueAccount.LastRefreshToken;
        this.IsAdmin = uniqueAccount.IsAdmin;
    }

    public string Id { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string DisplayName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string LastRefreshToken { get; private set; } = string.Empty;
    public bool IsAdmin { get; private set; }

    public static List<UniqueAccountResultDTO>? GenerateListFromAggregates(List<UniqueAccountAggregate>? aggregateList)
    {
        return aggregateList?.Select(x => new UniqueAccountResultDTO(x)).ToList();
    }
}
