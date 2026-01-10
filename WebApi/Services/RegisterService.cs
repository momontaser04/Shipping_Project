using AutoMapper;
using BL.Contract;
using BL.Contract.Shipment;
using BL.Mapping;
using BL.Service;
using BL.Service.Shipment;
using BL.Services;
using BL.Services.Shipment;
using DAL.Contract;
using DAL.DbContext_;
using DAL.Repository;
using DAL.UserModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Shipping_Project.Services;
using System.Text;

namespace WebApi.Services
{
    public class RegisterService
    {
        public static void Register(WebApplicationBuilder builder)
        {

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
          .AddCookie(options =>
          {
              options.LoginPath = "/login";
              options.AccessDeniedPath = "/access-denied";
          });

         

            builder.Services.AddDbContext<ShippingContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ShippingContext>()
    .AddDefaultTokenProviders();
            var jwtSecretKey = builder.Configuration.GetValue<string>("JwtSettings:SecretKey");
            var key = Encoding.ASCII.GetBytes(jwtSecretKey!);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
            builder.Services.AddAuthorization();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "   Hospital API",
                    Description = "Shipment Tracking System",
                    Contact = new OpenApiContact
                    {
                        Name = "MoMontaser",
                        Email = "momntaser99@gmail.com"
                    }
                });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter the JWT token in the format: Bearer {your token}",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                options.AddSecurityDefinition("Bearer", securityScheme);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, new List<string>() }
            });
            });


            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(MappingProfile).Assembly);
            });
            var mapper = config.CreateMapper();
            builder.Services.AddSingleton(mapper);
            Log.Logger = new LoggerConfiguration()
                     .WriteTo.Console()
                     .WriteTo.MSSqlServer(
                         connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
                         tableName: "Log",
                         autoCreateSqlTable: true)
                     .CreateLogger();
            builder.Host.UseSerilog();

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IViewRepository<>), typeof(ViewRepository<>));
            builder.Services.AddScoped<IShippingType, ShippingType>();
            builder.Services.AddScoped<ICountries, CountriesService>();
            builder.Services.AddScoped<ICities, CitiesService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRefreshToken, RefreshTokenService>();
            builder.Services.AddScoped<IRateCalculator, RateCalculatorService>();
            builder.Services.AddScoped<IShipment, ShipmentService>();
            builder.Services.AddScoped<ITrackingNumberCreator, TrackingNumberCreatorService>();
            builder.Services.AddScoped<IPaymentMethods, PaymentMethodsService>();
            builder.Services.AddScoped<IPackgingTypes, ShipingPackgingService>();
            builder.Services.AddScoped<IUserReceiver, UserReceiverService>();
            builder.Services.AddScoped<IUserSender, UserSenderService>();
            builder.Services.AddScoped<IShipmentStatus, ShipmentStatusService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IRefreshTokenRetriver, RefreshTokenRetriverService>();


            builder.Services.AddSingleton<TokenService>();





        }
    }
}
