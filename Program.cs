using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Ajouter les services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ⚡ CORS pour autoriser ton frontend local si nécessaire
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://127.0.0.1:8080") // adresse de ton front local
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// 🔹 Swagger (API documentation)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔹 Middleware
app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthorization();

// 🔹 Servir le frontend statique
var indexPath = Path.Combine(Directory.GetCurrentDirectory(), "index.html");
if (File.Exists(indexPath))
{
    app.UseDefaultFiles(); // index.html par défaut
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory()),
        RequestPath = "" // accessible depuis la racine
    });
}

// 🔹 Routes API
app.MapControllers();

// ⚡ Render fournit le port automatiquement
var port = Environment.GetEnvironmentVariable("PORT") ?? "5083";
app.Urls.Add($"http://0.0.0.0:{port}");

app.Run();