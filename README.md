# Devies Reads API

Bokaffär API byggd med .NET 9 Minimal API och MongoDB Atlas.

## Funktioner

- **Bokhantering**: CRUD-operationer för böcker med lagerhantering
- **Köpfunktionalitet**: Köp böcker med automatisk lagervalidering
- **Sökfunktion**: Sök böcker efter titel eller författarnamn
- **Författare & Kategorier**: Hantera författare och bokkategorier
- **API Key Authentication**: Säker åtkomst med API-nyckel
- **Swagger UI**: Interaktiv API-dokumentation

## Teknisk Stack

- **.NET 9.0** - ASP.NET Core Minimal API
- **MongoDB Atlas** - Cloud database
- **Swashbuckle** - OpenAPI/Swagger dokumentation
- **xUnit** - Enhetstester
- **FluentAssertions** - Test assertions
- **Coverlet** - Code coverage

## Kom igång

### Förutsättningar

- .NET 9 SDK
- MongoDB Atlas konto (eller lokal MongoDB)

### Installation

1. Klona repot:

```bash
git clone https://github.com/KennySohl/DeviesReadsAPI.git
cd DeviesReadsAPI
```

2. Konfigurera API-nyckel och MongoDB connection string i `appsettings.Development.json`:

```json
{
  "ApiKey": "DIN_API_NYCKEL",
  "BookstoreDatabase": {
    "ConnectionString": "mongodb+srv://..."
  }
}
```

3. Kör projektet:

```bash
dotnet run
```

4. Öppna Swagger UI:

```
http://localhost:5161/swagger
```

## API Endpoints

### Böcker

- `GET /api/books` - Hämta alla böcker
- `GET /api/books/{id}` - Hämta specifik bok
- `GET /api/books?searchTerm={term}` - Sök böcker
- `POST /api/books` - Skapa ny bok
- `PUT /api/books/{id}` - Uppdatera bok
- `DELETE /api/books/{id}` - Ta bort bok
- `POST /api/books/{id}/purchase` - Köp bok

### Författare

- `GET /api/authors` - Hämta alla författare
- `GET /api/authors/{id}` - Hämta specifik författare
- `POST /api/authors` - Skapa ny författare

### Kategorier

- `GET /api/categories` - Hämta alla kategorier
- `GET /api/categories/{id}` - Hämta specifik kategori
- `POST /api/categories` - Skapa ny kategori

## Authentication

Alla endpoints (utom Swagger) kräver en API-nyckel i headern:

```
X-API-Key: DIN_API_NYCKEL
```

I Swagger UI: Klicka på "Authorize" och ange din API-nyckel.

## Tester

### Köra tester

```bash
cd DeviesReadsAPI.Tests
dotnet test
```

### Code Coverage

```bash
cd DeviesReadsAPI.Tests
.\run-coverage.ps1
```

Detta kommer att:

1. Köra alla tester med coverage-analys
2. Generera en HTML-rapport
3. Öppna rapporten i din webbläsare

**Testtäckning:** 16 integrationstester som täcker alla endpoints, API key authentication, och köplogik.

## Projektstruktur

```
DeviesReadsAPI/
├── Models/              # Datamodeller (Book, Author, Category)
├── Services/            # Business logic (BookService)
├── Middleware/          # API Key authentication
├── Program.cs           # API endpoints och konfiguration
└── appsettings.json     # Konfiguration

DeviesReadsAPI.Tests/
├── BookApiTests.cs      # Tester för book endpoints
├── AuthorApiTests.cs    # Tester för author endpoints
├── CategoryApiTests.cs  # Tester för category endpoints
└── run-coverage.ps1     # Coverage script
```

## Licens

MIT License
