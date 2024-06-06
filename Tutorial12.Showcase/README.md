# Tutorial 12

## Introduction

The aim of the tutorial is show how to add to ASP.NET Core Web API project authentication and authorization. Also, not 
least important, this project shows example of creating of middleware that allows to handle errors globally.

## Nuget dependencies

- Microsoft.AspNetCore.Identity.EntityFrameworkCore - add authentication with help of EF Core
- Microsoft.EntityFrameworkCore.SqlServer - to make EF Core work with SQL Server
- Microsoft.EntityFrameworkCore.Design - adding support of migration

## CLI tools dependencies

You have to install `dotnet-ef` tool in order to be able to use migration.

More you can learn [here](https://learn.microsoft.com/en-us/ef/core/cli/dotnet#installing-the-tools)

## How to launch

1. Create a appsettings.json
2. In created JSON file, insert next template:
```
{
    "ConnectionStrings": {
        "DockerServer": "Server=ipAddressToDatabase;Database=databaseNameByDefault;User Id=username;Password=password;"
    }
}
``` 
where you can rename DockerServer (don't forget to change it in Program.cs!) and insert your connection string to database.
3. In your .csproj file, set next parameter to false value:
```
<InvariantGlobalization>false</InvariantGlobalization>
```
Otherwise, you will get an exception
4. Execute migrations by next command:
```
dotnet ef database update
```
5. To launch it, just select http/https profile in your IDE or run next command in terminal (assuming that you launch it from root of repository):
```
dotnet run --project .\Tutorial12.Showcase\Tutorial12.RestApi.csproj
```