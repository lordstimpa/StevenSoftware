using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StevenSoftware.Server.Database;
using StevenSoftware.Server.Models;
using StevenSoftware.Server.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "StevenSoftware API", Version = "v1" });
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "StevenSoftware.Server.xml"));
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer <token>'"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://localhost:60064", "https://stevensoftware.se")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUserModel, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(builder.Configuration["Jwt:Key"])),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(5),
		NameClaimType = ClaimTypes.Name,
	};
	options.MapInboundClaims = false;
});

builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddScoped<SeedingService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    try
    {
        var canQueryUsers = await dbContext.Database.ExecuteSqlRawAsync("SELECT 1 FROM \"AspNetUsers\" LIMIT 1") == 1;

        if (canQueryUsers)
        {
            var seedingService = scope.ServiceProvider.GetRequiredService<SeedingService>();
            await seedingService.SeedAdminUser(scope.ServiceProvider);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Skipping seeding: {ex.Message}");
    }
}

Console.WriteLine($"Running in {builder.Environment.EnvironmentName} environment");

app.UseCors("AllowSpecificOrigin");
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StevenSoftware API v1"));
app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    Console.WriteLine($"Incoming request path: {context.Request.Path}");
    await next();
});


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();