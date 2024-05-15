# Tutorial 10

## Introduction

This project shows how to work with ORM framework "Entity Framework Code" with Database-first approach.
Tutorial10.RestApi is a modification of Tutorial6 showcase project. 

## Dependencies

This project uses "Microsoft.EntityFrameworkCore.SqlServer" NuGet package in order to work with database through
EF Core.

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
4. To launch it, just select http/https profile in your IDE or run next command in terminal (assuming that you launch it from root of repository):
```
dotnet run --project .\Tutorial10.Showcase\Tutorial10.RestApi.csproj
```

## Useful links

[Microsoft's guide](https://learn.microsoft.com/en-us/ef/core/)