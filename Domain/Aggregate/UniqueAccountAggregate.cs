namespace Domain.Aggregate;

public class UniqueAccountAggregate
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string LastRefreshToken { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
}
