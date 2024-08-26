using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MicrosoftAuth.POC.Backend;
using MicrosoftAuth.POC.Backend.Utils.Helper;
using System.Text;

internal class Program
{
    private static string SecretKey = "NogameNolife1234567890" + DateTime.Now.ToUniversalTime(); // todo: get this from somewhere secure

    private static SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configuração da autenticação
        builder.Services.AddAuthentication(options => {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        })
        .AddCookie(options =>
        {
            options.Cookie.Name = "LoginMicrosoftPoc";
        })
        .AddCookie("LoginMicrosoftPoc")
        .AddOpenIdConnect("Microsoft", options =>
        {
            options.ClientId = builder.Configuration["AzureAd:ClientId"];
            options.ClientSecret = builder.Configuration["AzureAd:ClientSecret"];
            options.Authority = $"{builder.Configuration["AzureAd:Instance"]}/{builder.Configuration["AzureAd:TenantId"]}/v2.0/";
            options.SignInScheme = "LoginMicrosoftPoc";
            options.ResponseType = "code";
            options.Scope.Add("openid");
            options.Scope.Add("profile");
            options.CallbackPath = "/signin-oidc";           
            options.SignedOutCallbackPath = "/signout-callback-oidc";
            options.RemoteSignOutPath = "/signout-oidc";
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = $"{builder.Configuration["AzureAd:Instance"]}/{builder.Configuration["AzureAd:TenantId"]}/v2.0/"
            };
        });

        // Injeção de dependência
        builder.Services.AddScoped<IJwtFactory, JwtFactory>();

        builder.Services.Configure<JwtIssuerOptions>(options =>
        {
            options.Issuer = builder.Configuration["JwtIssuerOptions:Issuer"];
            options.Audience = builder.Configuration["JwtIssuerOptions:Audience"];
            options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
        });

        var urlsAngular = builder.Configuration.GetSection("AngularStfcorpUrls").Get<AngularStfcorpUrls>();
        builder.Services.AddSingleton(urlsAngular);


        var urlsMicrosoftOAuth = builder.Configuration.GetSection("MicrosoftOAuthRedirects").Get<MicrosoftOAuthRedirects>();
        builder.Services.AddSingleton(urlsMicrosoftOAuth);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
        {

            options.AddPolicy("Development",
                  builder =>
                      builder
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .SetIsOriginAllowed(origin => true)
                      .AllowCredentials()
                      ); // allow credentials

            options.AddPolicy("Production",
                builder =>
                    builder
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowAnyOrigin()
                      ); // allow credentials
        });

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("Development");
        }

        app.MapControllers();

        app.Run();
    }
}