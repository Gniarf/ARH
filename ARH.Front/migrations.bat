:: cf https://learn.microsoft.com/fr-fr/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli
:: et https://jasonwatmore.com/post/2022/01/31/net-6-database-migrations-to-different-db-per-environment-sqlite-in-dev-sql-server-in-prod
dotnet build
:: pour utiliser ces scripts correctement, penser à décommenter le constructeur vide du DataContext concerné et à le faire UNIQUEMENT le temps de la migration

:: Création Sqlite
::set ASPNETCORE_ENVIRONMENT=Development
::dotnet-ef migrations add Holydays --context ARH.Front.Services.SqliteDataContext --output-dir Migrations/SqliteMigrations 

:: Destruction de la dernière migration Sqlite
::set ASPNETCORE_ENVIRONMENT=Development
::dotnet-ef migrations remove --context SqliteDataContext

:: Création SqlServer
set ASPNETCORE_ENVIRONMENT=Tests
dotnet-ef migrations add Holydays --context DataContext --output-dir Migrations/SqlServerMigrations

:: Destruction de la dernière migration SqlServer
::set ASPNETCORE_ENVIRONMENT=Tests
::dotnet-ef migrations remove --context DataContext 