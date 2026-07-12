# Training-WebAPI-ASP.NET

## Installation

Install packages

```shell
dotnet restore
```

Create migration file

```shell
dotnet ef migrations add <NameOfMigration>
```

Run the migration file to update databse

```shell
dotnet ef database update
```

Drop the database

```shell
dotnet ef database drop
```

Run the backend project in debug mode

```shell
dotnet run watch -c Debug
```

Build the project

´´´shell
dotnet build
´´´
