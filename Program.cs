using dotenv.net;
using DotnetAPIProject.Data;
using DotnetAPIProject.Services.Implementations;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

// Load environment variables from .env file
DotEnv.Load(options: new DotEnvOptions(probeLevelsToSearch: 3));

var builder = WebApplication.CreateBuilder(args);

// Configure logging first
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Get logger directly from builder.Logging instead of BuildServiceProvider
var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<Program>();

logger.LogInformation("Starting application...");
//// cauas hinh
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.RequireHttpsMetadata = false;
//    options.SaveToken = true;
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
//        ValidAudience = builder.Configuration["JwtSettings:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"]!))
//    };
//});
//xg
// Override configuration with environment variables
builder
    .Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "Dictionary API",
            Version = "v1",
            Description = "A simple dictionary API",
        }
    );
});

// Add Health Checks
builder.Services.AddHealthChecks();

// Add DbContext with logging
builder.Services.AddDbContext<ApplicationDbContext>(
    (serviceProvider, options) =>
    {
        var connectionString =
            Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
            ?? builder.Configuration.GetConnectionString("DefaultConnection");

        logger.LogInformation(
            "Attempting database connection with connection string starting with: {ConnectionStart}...",
            connectionString?.Substring(0, Math.Min(connectionString?.Length ?? 0, 30))
        );

        options
            .UseSqlServer(connectionString)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            .LogTo(message => logger.LogInformation("{SqlMessage}", message), LogLevel.Information);

        // Test the connection immediately
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder
            .UseSqlServer(connectionString)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            .LogTo(message => logger.LogInformation("{SqlMessage}", message), LogLevel.Information);

        try
        {
            using var context = new ApplicationDbContext(optionsBuilder.Options);
            context.Database.OpenConnection();
            logger.LogInformation("✅ Database connection test successful");

            // Test if database exists
            var dbExists = context.Database.CanConnect();
            logger.LogInformation("Database exists and is accessible: {DbExists}", dbExists);

            // Ensure database is created
            context.Database.EnsureCreated();
            logger.LogInformation("✅ Database schema is ready");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Database connection failed. Error: {ErrorMessage}", ex.Message);
            // Rethrow to prevent application from starting with bad database connection
            throw;
        }
    }
);

// Add Services
builder.Services.AddScoped<IDictionaryService, DictionaryService>();
builder.Services.AddScoped<IWorkspaceService, WorkspaceService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ILoginService, LoginService>();
//builder.Services.AddScoped<IForgotPasswordService, ForgotPasswordService>();
builder.Services.AddScoped<IForgotPasswordService, ForgotPasswordService>();

//builder.Services.AddScoped<IJwtService, JwtService>();



// Add OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    logger.LogInformation("Running in Development mode");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Map health check endpoint
app.MapHealthChecks("/health");

logger.LogInformation("Application configured and ready to start");

app.Run();
