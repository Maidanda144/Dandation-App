using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Ajouter les services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS (si ton frontend fait des requêtes depuis un autre domaine)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware
app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthorization();

// Servir le frontend statique
var frontendPath = Path.Combine(Directory.GetCurrentDirectory(), "frontend");

if (Directory.Exists(frontendPath))
{
    app.UseDefaultFiles(new DefaultFilesOptions
    {
        FileProvider = new PhysicalFileProvider(frontendPath),
        RequestPath = ""
    });
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(frontendPath),
        RequestPath = ""
    });
}

// Routes API
app.MapControllers();

// Render fournit le port automatiquement
var port = Environment.GetEnvironmentVariable("PORT") ?? "5083";
app.Urls.Add($"http://0.0.0.0:{port}");

app.Run();