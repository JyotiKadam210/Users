# Users
This application is generating a .NET Core Web Api which stores, updates and retrieves Users information.

## Data Model
### User
| Property                  | Type    |
| ------------------------- | ------- |
| FirstName                 | string  |
| LastName                  | string  |
| Email                     | string  |
| DateOfBirth               | DateTime|
| Name                      | string  |


## Technical Stacks 

| Technical Stacks                  |
| ------------------------- |
| Web API Core 6.0 | 
| EF 7.0.9 Code First Approach | 
| SQLite Database | 
| Dependency Injection | 
| Linq | 
| Swagger - Validate API | 
| Unit Tests | 

## To Run this application

* Download the source code from the GitHub Page.
* Configure the DB Path in the appsettings.json file in VS solution 
  "ConnectionStrings": { "DefaultConnection":"Data Source = {Local Path}\\UserDb.db" }
* Install SQLite from https://sqlitebrowser.org/.
* Once the above configuration is done, you are good to run with the solution.

## Additional Commands
Restore Nuget Packages for this solution from  Nuget Package Manager =>Package Manager Console in VSCode.

* Packages to install
    1. dotnet add package Microsoft.EntityFrameworkCore
    2. dotnet add package Microsoft.EntityFrameworkCore.Design
    3. dotnet add package Microsoft.EntityFrameworkCore.Sqlite
    4. dotnet add package Microsoft.EntityFrameworkCore.Tools
    5. dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
    6. dotnet add package Swashbuckle.AspNetCore

* Install & Update dotnet EF tool
   1.  dotnet tool install --global dotnet-ef
   2. dotnet tool update --global dotnet-ef

* Code first approach SQLite Database
  1. dotnet ef migrations add Initials
  2. dotnet ef database update

        If any issue while genearting database, Update database connection string in UserDbContext

## Swagger samples 

The samples for posting and updating the related endpoints are provided below.
### User - Post & Put
```bash
{
    "userId": 1,
    "firstName": "Jyoti",
    "lastName": "Kadam",
    "email": "jyotik@gmail.com",
    "dateOfBirth": "1986-02-06T00:00:00"
}	
```
### User - Get
```bash
{  
    "userId": 1,
    "firstName": "Jyoti",
    "lastName": "Kadam",
    "email": "jyotik@gmail.com",
    "dateOfBirth": "1986-02-06T00:00:00",
    "age": 37
}
```
