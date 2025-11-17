using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");
app.UseHttpsRedirection();

// ---- SERVE FRONTEND ----
var frontendPath = Path.Combine(Directory.GetCurrentDirectory(), "frontend");

if (Directory.Exists(frontendPath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(frontendPath),
        RequestPath = ""
    });

    // SPA fallback
    app.Use(async (context, next) =>
    {
        if (!context.Request.Path.Value.StartsWith("/api"))
        {
            context.Response.ContentType = "text/html";
            await context.Response.SendFileAsync(Path.Combine(frontendPath, "index.html"));
            return;
        }
        await next();
    });
}

app.MapControllers();

// Render port binding
var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
app.Run($"http://0.0.0.0:{port}");