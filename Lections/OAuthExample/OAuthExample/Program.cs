using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = "GitHub";
})
    .AddCookie()
    .AddOAuth("GitHub", options =>
    {
        options.ClientId = builder.Configuration["Github:ClientId"];
        options.ClientSecret = builder.Configuration["Github:Secret"];

        options.CallbackPath = "/signin-github";

        options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
        options.TokenEndpoint = "https://github.com/login/oauth/access_token";
        options.UserInformationEndpoint = "https://api.github.com/user";

        options.SaveTokens = true;

        options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
        options.ClaimActions.MapJsonKey(ClaimTypes.Name, "login");

        options.Scope.Add("repo");

        options.Events = new OAuthEvents
        {
            OnCreatingTicket = async context =>
            {
                var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

                request.Headers.UserAgent.ParseAdd("OAuthExample");

                var response = await context.Backchannel.SendAsync(request);
                response.EnsureSuccessStatusCode();

                using var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

                context.RunClaimActions(user.RootElement);
            }

        };
    });

//builder.Services.AddAuthorization();

var app = builder.Build();
app.UseAuthentication();

app.MapGet("/login", async context =>
{
    await context.ChallengeAsync("GitHub", new AuthenticationProperties { RedirectUri = "/" });


});

app.MapGet("/", (HttpContext context) =>
{
    if (context.User.Identity?.IsAuthenticated == true)
    {
        var name = context.User.Identity.Name;
        return Results.Ok($" Hi, {name}!");
    }
    else
    {
        return Results.Unauthorized();
    }
});

app.MapGet("/repo", async (HttpContext context) =>
{
    if (context.User.Identity?.IsAuthenticated == true)
    {
        var token = await context.GetTokenAsync("access_token");

        using var http = new HttpClient();
        using var request = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/user/repos");

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        request.Headers.UserAgent.ParseAdd("OAuthExample");

        var response = await http.SendAsync(request);
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var repos = json.RootElement.EnumerateArray().ToArray();

        return Results.Ok(repos);
    }
    else
    {
        return Results.Unauthorized();
    }
});


app.Run();

