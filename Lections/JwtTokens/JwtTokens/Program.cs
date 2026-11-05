using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var key = new SymmetricSecurityKey(
    Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,

            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],

            RequireExpirationTime = true,
            ValidateLifetime = true,
            RequireSignedTokens = true,
            ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },

            ClockSkew = TimeSpan.FromSeconds(30)
        };
    });

builder.Services.AddAuthentication();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/token", (string username, string role, HttpContext context) =>
    {
        var token = CreateToken(context.RequestServices.GetRequiredService<IConfiguration>(), username, role);
        return Results.Ok(token);
    });

app.MapGet("/hello", (ClaimsPrincipal user) =>
{
    return Results.Ok(
        new
        {
            Username = user.Identity.Name
        });
}).RequireAuthorization(policy => policy.RequireRole("Admin"));

app.Run();


static string CreateToken(IConfiguration configuration, string username, string role)
{
    var claims = new[]
    {
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.Role, role)
    };

    var key = new SymmetricSecurityKey(
        Encoding.ASCII.GetBytes(configuration["Jwt:Key"]));

    var credetionals = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: configuration["Jwt:Issuer"],
        audience: configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(15),
        signingCredentials: credetionals);

    return new JwtSecurityTokenHandler().WriteToken(token);
}