using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Service;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Description = "Standard Authorization hearder using the Bearer Scheme (\"Bearer {token}\")",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
        });
        options.OperationFilter<SecurityRequirementsOperationFilter>();
    });
var service = builder.Services;

service.AddDbContext<DatabaseContext>();
service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});
service.AddAuthorization();

#region DI Repositories
service.AddScoped<IUserRepository, UserRepository>();
service.AddScoped<IMangaRepository, MangaRepository>();
service.AddScoped<IChapterService, ChapterService>();
service.AddScoped<IAuthorRepository, AuthorRepository>();
#endregion

#region DI Services
service.AddScoped<IAuthorService, AuthorService>();
service.AddScoped<IUserService, UserService>();
service.AddScoped<ISecureService, SecureService>();
service.AddScoped<IMangaService, MangaService>();
service.AddScoped<IChapterService, ChapterService>();
#endregion


//service.AddIdentityCore<UserEntity>(options =>
//{
//    options.SignIn.RequireConfirmedAccount = true;
//    options.Password.RequireDigit = true;
//    options.Password.RequireNonAlphanumeric = true;
//    options.Password.RequireUppercase = true;
//    options.Password.RequiredLength = 6;
//    options.Password.RequiredUniqueChars = 0;
//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//    options.Lockout.MaxFailedAccessAttempts = 5;
//    options.Lockout.AllowedForNewUsers = true;
//    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwsyzABCDEFGHIJKLMNOPQRSTUVWXYZ123456789-_.@";
//    options.User.RequireUniqueEmail = true;
//}).AddEntityFrameworkStores<DatabaseContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
//app.MapIdentityApi<UserEntity>();
app.MapControllers();

app.UseCors(builder =>
{
    builder
.AllowAnyOrigin().AllowAnyMethod()
    .AllowAnyHeader();
});
app.Run();
