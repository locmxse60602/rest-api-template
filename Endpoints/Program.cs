using EntityFrameworkCore.PostgreSQL;
using EntityFrameworkCore.Sqlite;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var provider = builder.Configuration.GetValue<string>("DatabaseProvider");

    if ("Sqlite".Equals(provider))
    {
        options.UseSqlite(
            builder.Configuration.GetConnectionString("Sqlite")!,
            x => x.MigrationsAssembly(typeof(Sqlite).Assembly.GetName().Name)
        );
    }
    if ("Postgres".Equals(provider)) {
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("Postgres")!,
            x => x.MigrationsAssembly(typeof(Postgres).Assembly.GetName().Name)
        );
    }
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();