using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Restapi_net8.Data;
using Restapi_net8.Infrastructure.DependencyInjection;
using Restapi_net8.Middlewares;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add services to the container.
builder.Services.AddCors(option => {
    option.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CodePulseCollectionString"));
});
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddAutoMapper();
builder.Services.AddMiddlewares();
builder.Services.AddInfrastructure();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => {
    o.SwaggerDoc("v1", new() { Title = "Restapi_net8", Version = "v1" });
    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme{
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme"
    });
    // o.AddSecurityRequirement(new OpenApiSecurityRequirement{
    //     {
    //         new OpenApiSecurityScheme{
    //             Reference = new OpenApiReference{
    //                 Type = ReferenceType.SecurityScheme,
    //                 Id = "Bearer"
    //             }
    //         },
    //         new string[]{}
    //     }
    // });
    
} );
builder.Services.AddAuthorization();
var secretKey = builder.Configuration["AppSettings:PrivateKey"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o => {
        o.RequireHttpsMetadata = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:PrivateKey"])),
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = false, 
            ValidateAudience = false,
        };
    });
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();
builder.Host.UseSerilog();
builder.Services.AddStackExchangeRedisCache(option => {
    string connectionString = builder.Configuration.GetConnectionString("Redis");
    option.Configuration = connectionString;
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseMiddleware<HttpExceptionHandlingMiddleware>();
app.UseMiddleware<UnauthorizeMiddleware>();
app.UseMiddleware<ForbidenMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
