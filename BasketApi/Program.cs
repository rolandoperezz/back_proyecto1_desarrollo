using Microsoft.EntityFrameworkCore;
using BasketApi.Data;
using BasketApi.Dtos;
using BasketApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ==== DB: SQL Server ====
// Lee ConnectionStrings:Default (appsettings o variables de entorno).
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var cs = builder.Configuration.GetConnectionString("Default")
             ?? "Server=sqlserver,1433;Database=BasketDb;User Id=sa;Password=YourStrong!Passw0rd;Encrypt=True;TrustServerCertificate=True;";
    options.UseSqlServer(cs, sql => sql.EnableRetryOnFailure());
});

// ==== Swagger ====
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ==== CORS ====
// Permite configurar orígenes por appsettings o env (Cors:AllowedOrigins="http://localhost:4200;https://miapp.com").
var allowedOrigins = builder.Configuration["Cors:AllowedOrigins"]?
    .Split(';', StringSplitOptions.RemoveEmptyEntries)
    ?? new[] { "http://localhost:4200" };

builder.Services.AddCors(options =>
{
    options.AddPolicy("ng-dev", p => p.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("ng-dev");

// ==== Aplicar migraciones al arrancar ====
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// ==== Endpoints mínimos ====

// Home → Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

// Guardar resultado final
app.MapPost("/api/matches", async (SaveMatchRequest req, AppDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(req.HomeName) || string.IsNullOrWhiteSpace(req.AwayName))
        return Results.BadRequest("Los nombres de equipo son obligatorios.");
    if (req.HomeScore < 0 || req.AwayScore < 0)
        return Results.BadRequest("Los puntajes no pueden ser negativos.");

    var entity = new MatchResult
    {
        HomeName = req.HomeName.Trim().ToUpperInvariant(),
        AwayName = req.AwayName.Trim().ToUpperInvariant(),
        HomeScore = req.HomeScore,
        AwayScore = req.AwayScore,
        HomeFouls = req.HomeFouls ?? 0,
        AwayFouls = req.AwayFouls ?? 0,
        QuarterDurationSec = req.QuarterDurationSec ?? 600,
        QuartersPlayed = req.QuartersPlayed ?? 4,
        HomeColorHex = NormalizeHex(req.HomeColorHex),
        AwayColorHex = NormalizeHex(req.AwayColorHex),
        ExtraJson = req.ExtraJson,
        EndedAtUtc = DateTime.UtcNow
    };

    db.MatchResults.Add(entity);
    await db.SaveChangesAsync();

    return Results.Created($"/api/matches/{entity.Id}", MatchResultDto.FromEntity(entity));
});

// Listar
app.MapGet("/api/matches", async (AppDbContext db) =>
    (await db.MatchResults
        .OrderByDescending(x => x.EndedAtUtc)
        .ToListAsync()
    ).Select(MatchResultDto.FromEntity).ToList()
);

// Por Id
app.MapGet("/api/matches/{id:guid}", async (Guid id, AppDbContext db) =>
{
    var e = await db.MatchResults.FindAsync(id);
    return e is null ? Results.NotFound() : Results.Ok(MatchResultDto.FromEntity(e));
});

// Eliminar (opcional)
app.MapDelete("/api/matches/{id:guid}", async (Guid id, AppDbContext db) =>
{
    var e = await db.MatchResults.FindAsync(id);
    if (e is null) return Results.NotFound();
    db.MatchResults.Remove(e);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();

// Helpers
static string? NormalizeHex(string? c)
{
    if (string.IsNullOrWhiteSpace(c)) return null;
    c = c.Trim();
    return c.StartsWith("#") ? c : "#" + c;
}
