using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Repositories;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var service = builder.Services;

service.AddDbContext<DatabaseContext>();
service.AddAuthorization();

#region DI Repositories
service.AddScoped<IUserRepository, UserRepository>();
service.AddScoped<ILoginRepository, LoginRepository>();
#endregion

#region DI Services
service.AddScoped<IUserService, UserService>();
#endregion


service.AddIdentityApiEndpoints<UserEntity>().AddEntityFrameworkStores<DatabaseContext>();
service.AddIdentityCore<UserEntity>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwsyzABCDEFGHIJKLMNOPQRSTUVWXYZ123456789-_.@";
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<DatabaseContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapIdentityApi<UserEntity>();
app.MapControllers();

app.Run();
