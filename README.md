# CheckLinksConsole

To Build database server on osx:
```
docker run -d -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=whatever12!' -p 1433:1433 --name sqllinux-netcore microsoft/mssql-server-linux
```

Run the following SQL to create the Links database and table:
```
CREATE DATABASE Links;
GO

CREATE TABLE Links.dbo.Links
    (ID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    Link text NULL,
    CheckedAt datetime NULL,
    Problem text NULL)
GO

```

Consider using flyway as an all SQL database migration tool to manage schema over time.

To run app:
```
# cd to app root directory
dotnet run
```