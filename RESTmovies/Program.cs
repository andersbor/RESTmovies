using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MoviesRepositoryLib;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Boolean useDatabase = true;
IMoviesRepository _repo;

if (useDatabase)
{
    var optionsBuilder = new DbContextOptionsBuilder<MoviesDbContext>();
    // https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets
    optionsBuilder.UseSqlServer(Secrets.ConnectionStringSimply);
    // connection string structure
    //   "Data Source=mssql7.unoeuro.com;Initial Catalog=FROM simply.com;Persist Security Info=True;User ID=FROM simply.com;Password=DB PASSWORD FROM simply.com;TrustServerCertificate=True"
    MoviesDbContext _dbContext = new(optionsBuilder.Options);
    _repo = new MoviesRepositoryDB(_dbContext);
}
else
{
    _repo = new MoviesRepositoryList();
}
builder.Services.AddSingleton<IMoviesRepository>(_repo);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAll");

app.Run();
