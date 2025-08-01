using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Application.AuthAccount;
using Application.InputParams;
using Application.RegisterAppServices;
using CInfrastructure.Dbconnection;
using CInfrastructure.DefaultData;
using CInfrastructure.Repository;
using e_commerce.MiddlewareExceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<CloudinarySetting>(builder.Configuration.GetSection("Cloudinary"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddTransient<Middleware1>();
builder.Services.ApplicationService();
builder.Services.InfractureService();
#region   Database connection 
builder.Services.AddDbContext<ApplicationDbcontext>( option =>

    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddIdentityApiEndpoints<User>(opt =>
{
    opt.User.RequireUniqueEmail = true;
}).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbcontext>().AddDefaultTokenProviders();
#endregion

#region frontend connection

builder.Services.AddCors(opt=>opt.AddPolicy("Policy",p=>p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

#endregion


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata=true;
    var jwtsetting = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = jwtsetting.Issuer,
        ValidAudience = jwtsetting.Audience,
        ValidateIssuerSigningKey =true,
        IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtsetting.Key)),
    };


});

builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        Description = @"Jwt Authorization Header using Bearer Scheme.
                    Enter 'Bearer' [space] and then your token in the input below
                    Example :Bearer 123abcdf"

    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference= new OpenApiReference
            {
                Type =ReferenceType.SecurityScheme,
                Id="Bearer"
            },
            Scheme="Oauth2",
            Name="Bearer",
            In=ParameterLocation.Header
        },
        new List<string>()

        }

    });
});

#region DefaultData
static async Task UpdataSeedDataAsync(IHost host)
{
    using var scope=host.Services.CreateScope();
    var service=scope.ServiceProvider;

    try
    {
        var context = service.GetRequiredService<ApplicationDbcontext>();
        var usermanager = service.GetRequiredService<UserManager<User>>();
        var rolemanager = service.GetRequiredService<RoleManager<IdentityRole>>();
        if (context.Database.IsSqlServer())
        {
          await context.Database.MigrateAsync();
        }
        await SeedData.SeedataAsync(context,usermanager,rolemanager);
    }
    catch (Exception ex)
    {
        var logger= service.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "error occurred while migration or seeding the database");
    }
}
#endregion
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

app.UseMiddleware<Middleware1>();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors("Policy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await UpdataSeedDataAsync(app);

app.Run();
