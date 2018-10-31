# AspNetCoreApiStarter
An ASP.NET Core (v2.1) Web API project to quickly bootstrap new projects.  Includes Identity, JWT authentication w/ refresh tokens.

# Setup
- Uses Sql Server Express LocalDB (If using Visual Studio install it under Individual Components in the Visual Studio installer or install separately using [this link](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-2016-express-localdb?view=sql-server-2017).
- Apply database migrations to create the db.  From a command line within the *Web.Api.Infrastructure* project folder use the dotnet CLI to run : 
- <code>Web.Api.Infrastructure>**dotnet ef database update --context AppDbContext**</code>
- <code>Web.Api.Infrastructure>**dotnet ef database update --context AppIdentityDbContext**</code>

# Visual Studio
Open the solution file <code>AspNetCoreApiStarter.sln</code> and build/run.

# Visual Studio Code
Open the <code>src</code> folder and <code>F5</code> to build/run.

# Swagger Enabled
To explore and test the available APIs simply run the project and use the Swagger UI.

The available APIs include:
- POST `/api/accounts` - Creates a new user.
- POST `/api/auth/login` - Authenticates a user.
- POST `/api/auth/refreshtoken` - Refreshes expired access tokens.
- GET `/api/protected` - Protected controller for testing role-based authorization.

# Contact
mark@fullstackmark.com
 
