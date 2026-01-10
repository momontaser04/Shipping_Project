using AutoMapper;
using BL.Contract;
using BL.Mapping;
using BL.Service;
using DAL.Contract;
using DAL.DbContext_;
using DAL.Repository;
using DAL.UserModels;
using Domains.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Serilog;
using WebApi.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:7198") // 👈 Your MVC project URL
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // 👈 Required for cookies (refresh token)
    });
});
builder.Services.AddControllers();
builder.Services.AddOpenApi();
RegisterService.Register(builder);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var dbContext = services.GetRequiredService<ShippingContext>();


    await dbContext.Database.MigrateAsync();


    await ContextConfig.SeedDataAsync(dbContext, userManager, roleManager);
}
