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

// ---- SERVE FRONTEND ----
var frontendPath = Path.Combine(AppContext.BaseDirectory, "frontend");

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

    // SPA fallback
    app.Use(async (context, next) =>
    {
        if (!context.Request.Path.Value.StartsWith("/api"))
        {
            var file = Path.Combine(frontendPath, "index.html");
            if (File.Exists(file))
            {
                context.Response.ContentType = "text/html";
                await context.Response.SendFileAsync(file);
                return;
            }
        }
        await next();
    });
}

app.MapControllers();

// Render port binding
var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
app.Run($"http://0.0.0.0:{port}");