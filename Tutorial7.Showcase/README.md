# Tutorial 7

## Introduction

## Goals

There are next goals for that tutorial:

- Show how to use async/await in ADO.NET and ASP.NET Core
- Show how to use stored procedures in ADO.NET
- Make an example of project that sticks with solution rules

## How to launch it

1. Create appsettings.json
2. In created JSON file, insert next template:
```
{
    "ConnectionStrings": {
        "Docker": "Server=ipAddressToDatabase;Database=SchoolCatalog;User Id=username;Password=password;"
    }
}
```
where you can rename DockerServer (don't forget to change it in Program.cs!) and insert your connection string to database.

**Attention!** 

Do not change database by default unless you understand what you're doing.

3. Create database "SchoolCatalog" and execute scripts (stored in folder "scripts") "create.sql", "insert.sql" and "proc.sql" into database.
4. In your .csproj file, set next parameter to false value:
```
<InvariantGlobalization>false</InvariantGlobalization>
```
Otherwise, you will get an exception

5. To launch it, just select http/https profile in your IDE or run next command in terminal (assuming that you launch it from root of repository):
```
dotnet run --project .\Tutorial7.Showcase\src\Tutorial7.Showcase.API\Tutorial7.Showcase.API.csproj
```