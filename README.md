## Team Hive

Modern, modular .NET 8 Clean Architecture backend for team/task management with JWT authentication, EF Core persistence, and Swagger/OpenAPI.

### Contents
- Overview
- Solution structure
- Features
- Tech stack
- Getting started
- Configuration
- Database & migrations
- Run & debug
- API usage (Swagger/JWT)
- Common tasks
- Troubleshooting
- Contributing

### Overview
TaskHive is a layered solution that separates concerns across Domain, Persistence, Services (application), Presentation (controllers), and a host application (`TaskHive`) that wires everything together. It supports multi-team management, task operations, and secure authentication via JWT.

### Solution structure
```
TaskHiveSolution/
  Domain/                       // Entities, contracts, exceptions
  Persistence/                  // EF Core DbContexts, repositories, migrations
  Services/                     // Application logic, specifications, mapping
  ServicesAbstraction/          // Service interfaces (abstractions)
  Shared/                       // DTOs, enums, shared models (e.g., JWT options)
  Presentation/                 // API controllers (Authentication, Teams, Tasks)
  TaskHive/                     // ASP.NET Core web host (Program, DI, Swagger, middleware)
```

### Features
- JWT-based authentication (register, login, check email, get user)
- Team lifecycle (create, get, update settings, delete, join/leave, members)
- Task lifecycle (create, update, delete, list, detail)
- Clean separation through repository/specification patterns
- Centralized error handling middleware and validation errors shaping
- AutoMapper-based mapping profiles
- Swagger/OpenAPI documentation

### Tech stack
- .NET 8, ASP.NET Core Web API
- Entity Framework Core
- Identity (separate identity DbContext)
- Swashbuckle (Swagger)
- AutoMapper

### Getting started
Prerequisites:
- .NET SDK 8.0+
- SQL Server (localdb, developer, or container)

Clone and restore:
```bash
git clone https://github.com/AhmedUsama29/Task-Hive.git
cd TaskHiveSolution
dotnet restore
```

Recommended first run target: `TaskHive` project (the host application).

### Configuration
Configuration lives in `TaskHive/appsettings.json` and `TaskHive/appsettings.Development.json`.

Required sections:
- `ConnectionStrings`: database connections for main and identity contexts
- `JWTOptions`: issuer, audience, secret key

Example (redact secrets in production):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=TaskHiveDb;Trusted_Connection=True;TrustServerCertificate=True;",
    "IdentityConnection": "Server=.;Database=TaskHiveIdentityDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "JWTOptions": {
    "Issuer": "TaskHive",
    "Audience": "TaskHive.Client",
    "SecretKey": "your-very-strong-secret-key-here"
  }
}
```

### Database & migrations
Projects:
- Main context: `Persistence/Data/TaskHiveDbContext.cs`
- Identity context: `Persistence/Identity/TaskHiveIdentityDbContext.cs`

Migrations live under:
- `Persistence/Data/DbMigrations`
- `Persistence/Identity/Migrations`

Typical workflow:
```bash
# From solution root
dotnet ef database update --project Persistence --startup-project TaskHive --context TaskHiveDbContext
dotnet ef database update --project Persistence --startup-project TaskHive --context TaskHiveIdentityDbContext
```

Seeding:
- On startup, `TaskHive/Extentions.cs` invokes `InitializeDbAsync()` to initialize identity data via `Persistence/DbInitializer.cs`.

### Run & debug
Run the host app:
```bash
dotnet run --project TaskHive
```

When running in Development, Swagger is enabled at:
- `https://localhost:<port>/swagger`

Startup highlights (`TaskHive` project):
- Registers infrastructure, application, and web services
- Adds controllers and plugs in the `Presentation` assembly as an application part
- Configures Swagger, JWT authentication, and global validation response factory
- Adds global exception handling middleware

### API usage (Swagger/JWT)
1) Open Swagger UI (Development): `https://localhost:<port>/swagger`
2) Register: `POST /api/Authentication/register`
3) Login: `POST /api/Authentication/login` → copy the returned JWT
4) Authorize in Swagger: click “Authorize” and enter `Bearer <your-token>`
5) Call secured endpoints under `Teams` and `Tasks`

Key endpoints (selection):
- Authentication
  - `POST /api/Authentication/register`
  - `POST /api/Authentication/login`
  - `GET /api/Authentication/emailExists?email=...`
  - `GET /api/Authentication/getUser` (requires auth)
- Teams (requires auth)
  - `GET /api/Teams/get/{teamId}`
  - `GET /api/Teams/get`
  - `POST /api/Teams/create`
  - `PUT /api/Teams/update/{teamId}`
  - `DELETE /api/Teams/delete/{teamId}`
  - `POST /api/Teams/join` (body: joinCode)
  - `POST /api/Teams/leave/{teamId}`
  - `GET /api/Teams/get/{teamId}/members`
  - `GET /api/Teams/get/member/{memberId}`
  - `DELETE /api/Teams/remove/{teamId}/{memberId}`
- Tasks (requires auth)
  - `GET /api/teams/{teamId}/Tasks`
  - `GET /api/teams/{teamId}/Tasks/{taskId}`
  - `POST /api/teams/{teamId}/Tasks/Create`
  - `PUT /api/teams/{teamId}/Tasks/{taskId}/Edit`
  - `DELETE /api/teams/{teamId}/Tasks/{taskId}/Delete`

Notes:
- The `TasksController` is nested under the route `api/teams/{teamId}/[controller]`.
- Most operations require a valid JWT in the `Authorization: Bearer <token>` header.

### Common tasks
- Add a new endpoint:
  - Add an action to a controller in `Presentation/Controllers`
  - Extend service logic in `Services` and interfaces in `ServicesAbstraction`
  - Add DTOs to `Shared` as needed
- Add a new entity:
  - Define model in `Domain/Models`
  - Add configuration in `Persistence/Data/Configurations`
  - Create migrations and update the database
  - Add specifications and mapping profiles

### Troubleshooting
- Swagger shows “No operations defined in spec!”
  - Ensure the `TaskHive` host adds controllers and includes the `Presentation` assembly as an application part
  - Confirm Development environment to enable Swagger
  - Clean and rebuild the solution
- 401/403 on secured endpoints
  - Provide a valid JWT via Swagger “Authorize” button
  - Verify `JWTOptions` configuration and clock sync
- EF Core tracking error (duplicate entity with same key)
  - Prefer attaching a single root entity and setting `EntityState.Modified` when updating a specific aggregate root
  - Avoid `DbSet.Update()` on large graphs if related navigations are already being tracked
- Database connection issues
  - Verify connection strings and SQL Server availability

### Contributing
1) Create a feature branch
2) Follow existing code style and layering conventions
3) Add/update unit/integration tests where applicable
4) Open a PR against `DEV` with a clear description

---
Maintained with ❤️ by Ahmed Osama.


