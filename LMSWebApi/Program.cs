using Contracts.Collections;
using Contracts.Extensions;
using Contracts.Utils;
using Domain.Context;
using Domain.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Services;
using StakeHoldersWebApi.Models.IdentityModels;
using LMSWebApi.Helpers;
using Contracts.Utils.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region DatabaseConfig
/* Database Context Dependency Injection */
var dbHost = "localhost";
var dbName = "MarthaLibraryDb";
var dbPassword = "entx!2003n";

var connectionString = $"server={dbHost};port=3306;database={dbName};user=root;password={dbPassword}";

var tt = builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseMySQL(connectionString));
builder.Services.AddScoped<DbInitializer>();
#endregion

#region Resovle dependency
// Configure the HTTP request pipeline.
builder.Services.AddSingleton<JwtTokenHandler>();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Lockout.AllowedForNewUsers = false;
    options.SignIn.RequireConfirmedEmail = true;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
//.AddDefaultUI()
.AddDefaultTokenProviders();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<ICacheManager, MemoryCacheManager>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); 


builder.Services.AddScoped<IServiceHelper, ServiceHelper>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<IWalletService, WalletService>();

builder.Services.AddTransient<IVendorService, VendorService>();
builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient<ILibraryBookService, LibraryBookService>();
builder.Services.AddTransient<IBookCheckService, BookCheckService>();

#endregion

#region AddSwaggerGen and Versioning
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Martha Library API (LMS's View)",

    });
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Token Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                       {
                         new OpenApiSecurityScheme
                         {
                           Reference = new OpenApiReference
                           {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                           }
                          },
                          new string[] { }
                        }
                      });
});


builder.Services.AddApiVersioning(config =>
{
    // Specify the default API Version as 1.0.0
    config.DefaultApiVersion = new ApiVersion(1, 0);
    // If the client hasn't specified the API version in the request, use the default API version number 
    config.AssumeDefaultVersionWhenUnspecified = true;
});

#endregion


builder.Services.AddCustomJwtAuthentication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseItToSeedSqlServer();    //custom extension method to seed the DB
    //configure other services
}


// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSwagger(
          c =>
          {
              c.RouteTemplate = "docs/{documentName}/docs.json";
          });
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint(builder.Configuration["Kestrel:Endpoints:Http:Url"] + "/docs/v1/docs.json", "LMS SSO Data");
    c.RoutePrefix = "";

});

app.Run();
