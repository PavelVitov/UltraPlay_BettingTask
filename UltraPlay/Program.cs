using Microsoft.EntityFrameworkCore;
using UltraPlay.Data;
using UltraPlay.Data.DTOs;
using UltraPlay.Services;
using UltraPlay.Services.DataProvider;
using UltraPlay.Services.Interfaces;
using UltraPlay.Services.Options;
using UltraPlay.Services.Parser;

// Create a new WebApplication using the provided command-line arguments (e.g., command-line arguments, environment variables).
var builder = WebApplication.CreateBuilder(args);

// Configure options for downloading data and specifying URLs from configuration.
builder.Services.Configure<DownloadOptions>(builder.Configuration.GetSection(DownloadOptions.Download));
builder.Services.Configure<UrlOptions>(builder.Configuration.GetSection(UrlOptions.Url));

// Add HttpClient for making HTTP requests to the SportsXMLProvider.
builder.Services.AddHttpClient<ISportsXMLProvider, SportsXMLProvider>();

// Register the SportsXMLParser as a singleton service, responsible for parsing sports data from XML.
builder.Services.AddSingleton<ISportsXMLParser<SportDTO[]>, SportsXMLParser>();

// Register services for updating the database and retrieving match data.
builder.Services.AddScoped<IDatabaseUpdater, DatabaseUpdater>();
builder.Services.AddScoped<IMatchRetrieverService, MatchRetrieverService>();

// Add a hosted service that synchronizes the database periodically.
builder.Services.AddHostedService<DbSyncService>();

// Configure and add the database context using Entity Framework Core with a SQL Server connection.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Add controllers for handling HTTP requests.
builder.Services.AddControllers();

// Add Swagger for API documentation.
builder.Services.AddSwaggerGen();

// Build the application.
var app = builder.Build();

// Configure the HTTP request pipeline.

// Enable Swagger and Swagger UI to provide API documentation.
app.UseSwagger();
app.UseSwaggerUI();

// Redirect HTTP to HTTPS for secure communication.
app.UseHttpsRedirection();

// Enable authorization if needed.
app.UseAuthorization();

// Map controllers to handle incoming HTTP requests.
app.MapControllers();

// Start the application.
app.Run();
