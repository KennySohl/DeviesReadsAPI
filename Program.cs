using DeviesReadsAPI.Models;
using DeviesReadsAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// - MongoDB settings
builder.Services.Configure<BookstoreDatabaseSettings>(
    builder.Configuration.GetSection("BookstoreDatabase"));
builder.Services.AddSingleton<BookService>();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

// - GET all books
app.MapGet("/api/books", async (BookService bookService) =>
{
    var books = await bookService.GetBooksAsync();
    return Results.Ok(books);
})
.WithName("GetAllBooks");

// - GET book by id
app.MapGet("/api/books/{id}", async (string id, BookService bookService) =>
{
    var book = await bookService.GetBookAsync(id);
    return book is not null ? Results.Ok(book) : Results.NotFound();
})
.WithName("GetBook");

// - POST new book
app.MapPost("/api/books", async (Book newBook, BookService bookService) =>
{
    await bookService.CreateBookAsync(newBook);
    return Results.Created($"/api/books/{newBook.Id}", newBook);
})
.WithName("CreateBook");

// - PUT update book
app.MapPut("/api/books/{id}", async (string id, Book updatedBook, BookService bookService) =>
{
    var book = await bookService.GetBookAsync(id);
    if (book is null) return Results.NotFound();
    
    updatedBook.Id = id;
    await bookService.UpdateBookAsync(id, updatedBook);
    return Results.NoContent();
})
.WithName("UpdateBook");

// - DELETE book
app.MapDelete("/api/books/{id}", async (string id, BookService bookService) =>
{
    var book = await bookService.GetBookAsync(id);
    if (book is null) return Results.NotFound();
    
    await bookService.RemoveBookAsync(id);
    return Results.NoContent();
})
.WithName("DeleteBook");

// - POST purchase book
app.MapPost("/api/books/{id}/purchase", async (string id, PurchaseRequest request, BookService bookService) =>
{
    var result = await bookService.PurchaseBookAsync(id, request.Quantity);
    return result.Success ? Results.Ok(result) : Results.BadRequest(result);
})
.WithName("PurchaseBook");

// - GET search books
app.MapGet("/api/books/search/{query}", async (string query, BookService bookService) =>
{
    var books = await bookService.SearchBooksAsync(query);
    return Results.Ok(books);
})
.WithName("SearchBooks");

// - GET books by category
app.MapGet("/api/books/category/{categoryId}", async (string categoryId, BookService bookService) =>
{
    var books = await bookService.GetBooksByCategoryAsync(categoryId);
    return Results.Ok(books);
})
.WithName("GetBooksByCategory");

// - Authors endpoints
app.MapGet("/api/authors", async (BookService bookService) =>
    Results.Ok(await bookService.GetAuthorsAsync()))
.WithName("GetAllAuthors");

app.MapGet("/api/authors/{id}", async (string id, BookService bookService) =>
{
    var author = await bookService.GetAuthorAsync(id);
    return author is not null ? Results.Ok(author) : Results.NotFound();
})
.WithName("GetAuthor");

app.MapPost("/api/authors", async (Author newAuthor, BookService bookService) =>
{
    await bookService.CreateAuthorAsync(newAuthor);
    return Results.Created($"/api/authors/{newAuthor.Id}", newAuthor);
})
.WithName("CreateAuthor");

// - Categories endpoints
app.MapGet("/api/categories", async (BookService bookService) =>
    Results.Ok(await bookService.GetCategoriesAsync()))
.WithName("GetAllCategories");

app.MapGet("/api/categories/{id}", async (string id, BookService bookService) =>
{
    var category = await bookService.GetCategoryAsync(id);
    return category is not null ? Results.Ok(category) : Results.NotFound();
})
.WithName("GetCategory");

app.MapPost("/api/categories", async (Category newCategory, BookService bookService) =>
{
    await bookService.CreateCategoryAsync(newCategory);
    return Results.Created($"/api/categories/{newCategory.Id}", newCategory);
})
.WithName("CreateCategory");

app.Run();
