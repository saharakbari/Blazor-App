using Api.Hubs;
using Application.Interfaces;
using Application.Mapper;
using Application.Services;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
using MediatR;
using Application.Commands;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add repository 
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<TaskItem>, TaskItemRepository>();


//add DBContext
builder.Services.AddDbContext<BlazorDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//AutoMapper
builder.Services.AddAutoMapper(typeof(TaskItemMapper).Assembly);

//service
builder.Services.AddScoped<TaskService>();



// config Jwt
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings.GetValue<string>("secret");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
        ValidAudience = jwtSettings.GetValue<string>("Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ClockSkew = TimeSpan.Zero
    };

});

builder.Services.AddAuthorization(options =>

{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireGuestRole", policy => policy.RequireRole("Guest"));
    options.AddPolicy("RequireCustomerRole", policy => policy.RequireRole("Customer"));

});


//service For Login
builder.Services.AddScoped<LoginUserService>(provider =>
    new LoginUserService(
        provider.GetRequiredService<IRepository<User>>(),
        secretKey
    ));

builder.Services.AddScoped<RegisterUserService>();
builder.Services.AddSignalR();

//////////////////////////////cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:7184") // آدرس Blazor
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// اتصال Serilog به سیستم ILogger
builder.Host.UseSerilog();


//// Add MediatR و مشخص کردن Assembly که Handlers داخلش هستن
builder.Services.AddMediatR(typeof(AddTaskCommandHandler).Assembly);


var app = builder.Build();

app.MapHub<TaskHub>("/taskHub");

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
