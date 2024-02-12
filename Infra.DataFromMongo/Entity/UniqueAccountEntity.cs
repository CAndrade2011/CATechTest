using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infra.DataFromMongo.Entity;

public class UniqueAccount
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    [BsonElement("name")]
    [BsonRepresentation(BsonType.String)]
    public string Name { get; set; }

    [BsonElement("displayName")]
    [BsonRepresentation(BsonType.String)]
    public string DisplayName { get; set; }

    [BsonElement("email")]
    [BsonRepresentation(BsonType.String)]
    public string Email { get; set; }

    [BsonElement("password")]
    [BsonRepresentation(BsonType.String)]
    public string Password { get; set; }

    [BsonElement("lastRefreshToken")]
    [BsonRepresentation(BsonType.String)]
    public string LastRefreshToken { get; set; }

    [BsonElement("isAdmin")]
    [BsonRepresentation(BsonType.Boolean)]
    public bool IsAdmin { get; set; }
}
