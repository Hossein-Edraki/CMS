using Application;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<User, IdentityRole>(o =>
{
    o.Password.RequireDigit = false;
    o.Password.RequireLowercase = false;
    o.Password.RequireUppercase = false;
    o.Password.RequireNonAlphanumeric = false;
    o.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

var _appConfig = builder.Configuration.GetValue<AppConfig>("AppConfig");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var secretkey = Encoding.UTF8.GetBytes(_appConfig.JwtOptions.SecretKey);
        var encryptionkey = Encoding.UTF8.GetBytes(_appConfig.JwtOptions.Encryptkey);

        var validationParameters = new TokenValidationParameters
        {
            ClockSkew = TimeSpan.Zero, // default: 5 min
            RequireSignedTokens = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretkey),

            RequireExpirationTime = true,
            ValidateLifetime = true,

            ValidateAudience = true, //default : false
            ValidAudience = _appConfig.JwtOptions.Audience,

            ValidateIssuer = true, //default : false
            ValidIssuer = _appConfig.JwtOptions.Issuer,

            TokenDecryptionKey = new SymmetricSecurityKey(encryptionkey)
        };

        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = validationParameters;
    });

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();