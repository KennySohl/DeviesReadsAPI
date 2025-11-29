using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeviesReadsAPI.Models;

public class Book
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("title")]
    public string Title { get; set; } = string.Empty;
    
    [BsonElement("isbn")]
    public string ISBN { get; set; } = string.Empty;
    
    [BsonElement("price")]
    public decimal Price { get; set; }
    
    [BsonElement("publishDate")]
    public DateTime? PublishDate { get; set; }
    
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;
    
    [BsonElement("coverUrl")]
    public string CoverUrl { get; set; } = string.Empty;
    
    [BsonElement("stockQuantity")]
    public int StockQuantity { get; set; }
    
    [BsonElement("authorId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string AuthorId { get; set; } = string.Empty;
    
    [BsonElement("categoryId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string CategoryId { get; set; } = string.Empty;
    
    [BsonElement("authorName")]
    public string? AuthorName { get; set; }
    
    [BsonElement("categoryName")]
    public string? CategoryName { get; set; }
}
