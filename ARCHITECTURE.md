# BookFlow

## Architecture

### API

- Controllers
- Program.cs
- appsettings.json

### Core

- Entities
- DTOs
- Interfaces

### BLL

- Services

### DAL

- Context
- Configurations
- Repositories
- Migrations

## Project References

BookFlow.Api
    -> BookFlow.Core
    -> BookFlow.BLL
    -> BookFlow.DAL

BookFlow.BLL
    -> BookFlow.Core

BookFlow.DAL
    -> BookFlow.Core

## Database

DbContext:

BookFlow.DAL/Context/AppDbContext.cs

Connection string:

DefaultConnection

## EF Core Commands

Create migration:

dotnet ef migrations add InitialCreate \
    --project BookFlow.DAL \
    --startup-project BookFlow.Api

Update database:

dotnet ef database update \
    --project BookFlow.DAL \
    --startup-project BookFlow.Api
