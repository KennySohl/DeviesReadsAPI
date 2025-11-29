namespace DeviesReadsAPI.Models;

public class BookstoreDatabaseSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string BooksCollectionName { get; set; } = string.Empty;
    public string AuthorsCollectionName { get; set; } = string.Empty;
    public string CategoriesCollectionName { get; set; } = string.Empty;
}
