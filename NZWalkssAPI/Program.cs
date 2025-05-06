using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NZWalkssAPI.Data;
using NZWalkssAPI.Mappings;
using NZWalkssAPI.Repositories;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using Serilog; 
using NZWalkssAPI.Middlewares;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/NZWalkss_Log.txt", rollingInterval: RollingInterval.Hour)
    .MinimumLevel.Information()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();



//injecting the Http Content Accessor
builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "NZWalkssAPI", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

//injected the NZWalkss Db Context
builder.Services.AddDbContext<NZWalkssDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalkssConnectionString")));

//injected the NZWalkss Auth DB Context
builder.Services.AddDbContext<NZWalkssAuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalkssAuthConnectionString")));

//Injected the SQL Region Repository
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();

//injected the Token Repository
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

//injected the I Image Repository
builder.Services.AddScoped<IImageRepository, LocalImageRepository>();

//Injected the automapper
builder.Services.AddAutoMapper(typeof(AutomapperProfiles));

//injected the identity packages
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalkss")
    .AddEntityFrameworkStores<NZWalkssAuthDbContext>()
    .AddDefaultTokenProviders();

//Password Configurations
builder.Services.Configure<IdentityOptions>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 10;
        options.Password.RequiredUniqueChars = 1;
    });

//Injected the SQL Walks Repository
builder.Services.AddScoped<IWalksRepository, SQLWalksRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true, 
    ValidateAudience = true, 
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    ValidAudience = builder.Configuration["Jwt:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//injecting the middleware called exception Handler Middleware
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

//injecting a new middleware so we can serve static files through ASP.NET Core Web API
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});

app.MapControllers();

app.Run();
