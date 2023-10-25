using System.Text;
using DineFine.Accessor;
using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.API.Filters;
using DineFine.API.Services;
using DineFine.Cache;
using DineFine.DataObjects.Entities;
using DineFine.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace DineFine.API;

public static class Bootstrapper
{
    public static void AddBootstrapper(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationDatabases(configuration);
        services.AddSessionAccessor();
        services.AddInMemoryCache(configuration);
        services.AddApplicationServices();
        services.AddIdentityService(configuration);
        
        services.AddApplicationControllersConfig();
        services.AddApplicationSwagger();
        services.AddHttpContextAccessor();
        services.AddEndpointsApiExplorer();
    }
    
    private static void AddApplicationControllersConfig(this IServiceCollection services)
    {
        services.AddControllers(
            options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().AddRequirements(new SessionExistsRequirement()).Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }
        );
        
        services.AddScoped<IAuthorizationHandler, SessionExistsHandler>();
    }
    
    private static void AddApplicationDatabases(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSqlContext(configuration);
        services.AddCosmosContext(configuration);
    }
    
    private static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<AuthService>();
        services.AddScoped<UserService>();
        services.AddScoped<UserRoleService>();
        services.AddScoped<RoleService>();
        
        services.AddScoped<UserSessionService>();
        
        services.AddScoped<TokenService>();
        
        services.AddScoped<EmailService>();
        services.AddScoped<IngredientService>();
        services.AddScoped<MenuItemService>();
        services.AddScoped<MenuItemIngredientService>();
        services.AddScoped<OrderService>();
        services.AddScoped<RestaurantService>();
        services.AddScoped<RestaurantCategoryService>();
        services.AddScoped<RestaurantStockInfoService>();
        services.AddScoped<TableOfRestaurantService>();
        services.AddScoped<TableSessionService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<ReportService>();
        services.AddScoped<CategoryService>();
    }
    
    private static void AddApplicationSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
            {
                Description = "Standard Authorization header using the Bearer scheme(\"bearer {token}\")",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Id = "oauth2",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            });
        });
    }
    
    private static void AddIdentityService(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtBearerTokenSection = configuration.GetSection(nameof(JwtBearerTokenSettings));
        var jwtBearerTokenSettings = jwtBearerTokenSection.Get<JwtBearerTokenSettings>();
        var jwtSecretKey = Encoding.ASCII.GetBytes(jwtBearerTokenSettings!.SecurityKey);
        
        services.AddIdentity<User, Role>(x =>
            {
                x.User.RequireUniqueEmail = true;
                x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(15);
                x.Lockout.MaxFailedAccessAttempts = 3;
            })
            .AddEntityFrameworkStores<MssqlContext>()
            .AddDefaultTokenProviders();
        
        services.Configure<DataProtectionTokenProviderOptions>(x =>
        {
            x.TokenLifespan = TimeSpan.FromHours(2);
        });
        
        services.Configure<JwtBearerTokenSettings>(jwtBearerTokenSection);

        services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new()
                {
                    ValidIssuer = jwtBearerTokenSettings.Issuer,
                    ValidAudience = jwtBearerTokenSettings.Audiences[0],
                    IssuerSigningKey = new SymmetricSecurityKey(jwtSecretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
    }
}