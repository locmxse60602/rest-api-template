using EntityFrameworkCore.MySQL;
using EntityFrameworkCore.PostgreSql;
using EntityFrameworkCore.Sqlite;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var provider = builder.Configuration.GetValue<string>("DatabaseProvider");

    switch (provider)
    {
        case "Sqlite":
            options.UseSqlite(
                builder.Configuration.GetConnectionString("Sqlite")!,
                x => x.MigrationsAssembly(typeof(Sqlite).Assembly.GetName().Name)
            );
            break;
        case "Postgres":
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("Postgres")!,
                x => x.MigrationsAssembly(typeof(Postgres).Assembly.GetName().Name)
            );
            break;
        case "MySql":
            var serverVersion = new MySqlServerVersion(builder.Configuration.GetValue<string>("ConnectionStringProps:MySqlVersion"));
            options.UseMySql(builder.Configuration.GetConnectionString("Postgres")!, serverVersion,
                x => x.MigrationsAssembly(typeof(MySql).Assembly.GetName().Name));
            break;
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
