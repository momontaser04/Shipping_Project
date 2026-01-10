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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Net.Http.Headers;

namespace Shipping_Project.Services
{
    public class RegisterService
    {
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient("ApiClient", client =>
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
          .AddCookie(options =>
          {
              options.LoginPath = "/login";
              options.AccessDeniedPath = "/access-denied";
              options.SlidingExpiration = true; ;
              options.Cookie.IsEssential = true;
              options.ExpireTimeSpan = TimeSpan.FromDays(7);
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

            builder.Services.AddAuthorization();
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


            builder.Services.AddScoped<GenericApiClient>();



        }
    }
}
