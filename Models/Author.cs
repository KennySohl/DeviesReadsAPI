using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeviesReadsAPI.Models;

public class Author
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;
    
    [BsonElement("biography")]
    public string Biography { get; set; } = string.Empty;
}
