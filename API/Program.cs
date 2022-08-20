using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<UrlContext>(options =>
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        
        string connStr;
        
        if (env == "Development")
        {
            connStr = builder.Configuration.GetConnectionString("DefaultConnection");
        }
        else
        {
            var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            
            connUrl = connUrl.Replace("postgres://", string.Empty);
            var pgUserPass = connUrl.Split("@")[0];
            var pgHostPortDb = connUrl.Split("@")[1];
            var pgHostPort = pgHostPortDb.Split("/")[0];
            var pgDb = pgHostPortDb.Split("/")[1];
            var pgUser = pgUserPass.Split(":")[0];
            var pgPass = pgUserPass.Split(":")[1];
            var pgHost = pgHostPort.Split(":")[0];
            var pgPort = pgHostPort.Split(":")[1];

            connStr = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};SSL Mode=Require;Trust Server Certificate=true";
        }

        options.UseNpgsql(connStr);
    });
builder.Services.AddScoped<IUrlRepo, UrlRepo>();
builder.Services.AddCors( config => config.AddPolicy(name: "Origin",
    policy => { 
        policy.WithOrigins("http://localhost:3000", "http://localhost:5144", "https://localhost:7276")
        .AllowAnyHeader().AllowAnyMethod(); }));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("Origin");

app.UseAuthorization();
app.UseEndpoints(endpoints => 
{
    app.MapControllers();
    app.MapFallbackToController("Index", "Fallback");
});
app.Run();
