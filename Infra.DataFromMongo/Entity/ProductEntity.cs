using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infra.DataFromMongo.Entity;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    [BsonElement("name")]
    [BsonRepresentation(BsonType.String)]
    public string Name { get; set; }

    [BsonElement("barCode")]
    [BsonRepresentation(BsonType.String)]
    public string BarCode { get; set; }

    [BsonElement("quantity")]
    [BsonRepresentation(BsonType.String)]
    public int Quantity { get; set; }
}
