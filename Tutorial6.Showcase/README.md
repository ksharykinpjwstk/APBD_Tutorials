# Tutorial 6 - Creation of Web API with usage of ADO.NET

## Introduction

ADO.NET is a library developed by Microsoft for accessing data in databases.
This project allows to get all cars, get car by ID or create a new one. 

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
dotnet run --project .\Tutorial6.Showcase\Tutorial6.Showcase.csproj
```

## Notes

BE ADVISED! In ipAddress, you can write just localhost in case if your PC run and docker/SQL Server and project the same time. So, your ip address should look something like that:
```
localhost,1433
```
If your port is different then 1433, then use mapped port.