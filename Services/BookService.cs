using DeviesReadsAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeviesReadsAPI.Services;

public class BookService
{
    private readonly IMongoCollection<Book> _booksCollection;
    private readonly IMongoCollection<Author> _authorsCollection;
    private readonly IMongoCollection<Category> _categoriesCollection;

    public BookService(IOptions<BookstoreDatabaseSettings> bookstoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(bookstoreDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(bookstoreDatabaseSettings.Value.DatabaseName);
        
        _booksCollection = mongoDatabase.GetCollection<Book>(bookstoreDatabaseSettings.Value.BooksCollectionName);
        _authorsCollection = mongoDatabase.GetCollection<Author>(bookstoreDatabaseSettings.Value.AuthorsCollectionName);
        _categoriesCollection = mongoDatabase.GetCollection<Category>(bookstoreDatabaseSettings.Value.CategoriesCollectionName);
    }

    // Books
    public async Task<List<Book>> GetBooksAsync() =>
        await _booksCollection.Find(_ => true).ToListAsync();

    public async Task<Book?> GetBookAsync(string id) =>
        await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<Book> CreateBookAsync(Book newBook)
    {
        await _booksCollection.InsertOneAsync(newBook);
        return newBook;
    }

    public async Task UpdateBookAsync(string id, Book updatedBook) =>
        await _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveBookAsync(string id) =>
        await _booksCollection.DeleteOneAsync(x => x.Id == id);

    public async Task<List<Book>> SearchBooksAsync(string query)
    {
        var filter = Builders<Book>.Filter.Or(
            Builders<Book>.Filter.Regex(x => x.Title, new MongoDB.Bson.BsonRegularExpression(query, "i")),
            Builders<Book>.Filter.Regex(x => x.AuthorName!, new MongoDB.Bson.BsonRegularExpression(query, "i"))
        );
        return await _booksCollection.Find(filter).ToListAsync();
    }

    public async Task<List<Book>> GetBooksByCategoryAsync(string categoryId) =>
        await _booksCollection.Find(x => x.CategoryId == categoryId).ToListAsync();

    public async Task<PurchaseResponse> PurchaseBookAsync(string id, int quantity)
    {
        var book = await GetBookAsync(id);
        
        if (book == null)
            return new PurchaseResponse { Success = false, Message = "Boken finns inte" };
        
        if (book.StockQuantity < quantity)
            return new PurchaseResponse 
            { 
                Success = false, 
                Message = $"Otillräckligt lager. Endast {book.StockQuantity} st tillgängliga",
                RemainingStock = book.StockQuantity
            };
        
        book.StockQuantity -= quantity;
        await UpdateBookAsync(id, book);
        
        return new PurchaseResponse 
        { 
            Success = true, 
            Message = $"Köp genomfört! {quantity} st köpta",
            RemainingStock = book.StockQuantity
        };
    }

    // Authors
    public async Task<List<Author>> GetAuthorsAsync() =>
        await _authorsCollection.Find(_ => true).ToListAsync();

    public async Task<Author?> GetAuthorAsync(string id) =>
        await _authorsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<Author> CreateAuthorAsync(Author newAuthor)
    {
        await _authorsCollection.InsertOneAsync(newAuthor);
        return newAuthor;
    }

    // Categories
    public async Task<List<Category>> GetCategoriesAsync() =>
        await _categoriesCollection.Find(_ => true).ToListAsync();

    public async Task<Category?> GetCategoryAsync(string id) =>
        await _categoriesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<Category> CreateCategoryAsync(Category newCategory)
    {
        await _categoriesCollection.InsertOneAsync(newCategory);
        return newCategory;
    }
}
