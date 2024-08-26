﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MicrosoftAuth.POC.Backend.Utils.Helper;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace MicrosoftAuth.POC.Backend.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        //private readonly IJwtHelperService _jwtHelperService;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly MicrosoftOAuthRedirects _microsoftOAuthRedirects;
        private readonly AngularStfcorpUrls _angularStfcorpUrls;

        public AuthController(
            IJwtFactory jwtFactory,
            IOptions<JwtIssuerOptions> jwtOptions,
            MicrosoftOAuthRedirects microsoftOAuthRedirects,
            AngularStfcorpUrls angularStfcorpUrls)
        {
            //_jwtHelperService = jwtHelperService;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _microsoftOAuthRedirects = microsoftOAuthRedirects;
            _angularStfcorpUrls = angularStfcorpUrls;
        }

        [AllowAnonymous]
        [HttpGet("microsoft-login")]
        public async Task Login()
        {
            // var redirectUri = !Debugger.IsAttached ? _microsoftOAuthRedirects.ExternalLoginHandlerRedirectUrl : _microsoftOAuthRedirects.ExternalLoginHandlerRedirectUrlLocalhost;
            var redirectUri = _microsoftOAuthRedirects.ExternalLoginHandlerRedirectUrl;
            Console.WriteLine("redirectUri: ", redirectUri);
            var authProperties = new AuthenticationProperties() { RedirectUri = redirectUri };
            await HttpContext.ChallengeAsync("Microsoft", authProperties);
        }

        [AllowAnonymous]
        [HttpGet("externalLoginHandler")]
        public async Task<IActionResult> ExternalLoginHandler()
        {
            var result = await HttpContext.AuthenticateAsync("LoginMicrosoftPoc");
            var stream = result.Properties!.Items.FirstOrDefault(x => x.Key == ".Token.id_token").Value;
            var name = result.Principal!.Claims.FirstOrDefault(c => c.Type == "name");
            var email = result.Principal!.Claims.FirstOrDefault(c => c.Type == "preferred_username");
            var login = email.Value.Split('@')[0];

            //var user = _jwtHelperService.GetUserFromToken(stream!);
            // var responseRepository = _usuarioService.GetUserTokenDataByLogin(user);
            // var celTratada = TratarCelula(responseRepository.Result.Celula);
            // var idPais = ObterIdPaisPorCelula(celTratada);
            // var celulasVisualizadas = ObterCelulasVisualizadas(responseRepository.Result.Login, Convert.ToInt32(celTratada));
            // var EAlogin = RetornaLoginPorRecurso(responseRepository.Result.IdEacesso);

            // var identity = _jwtFactory.GenerateClaimsIdentity(responseRepository.Result.Login, responseRepository.Result.NomeCompleto, responseRepository.Result.Email, "", responseRepository.Result.Celula, responseRepository.Result.IdEacesso, celulasVisualizadas, celTratada, EAlogin);
            var identity = _jwtFactory.GenerateClaimsIdentity(login, name!.Value, email!.Value);
            var jwt = await Tokens.GenerateJwtToken(identity, _jwtFactory, login, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });

            // //_jwtHelperService.TestesProblemaLogin(jwt, "ExternalLoginHandler");

            //return Ok(_angularStfcorpUrls.UrlPaginaPrincipal + jwt);
            //return Redirect(_angularStfcorpUrls.UrlPaginaPrincipal + "?token=" + jwt);
            return Redirect(_angularStfcorpUrls.UrlPaginaPrincipal);
        }

        [HttpPost("VerificarToken/")]
        public async Task<IActionResult> VerificarToken([FromBody] TokenViewModel token)
        {
            //var DtExpiramento = new DateTimeOffset();
            var tokenValido = new JwtSecurityTokenHandler().CanReadToken(token.Token);
            if (tokenValido)
            {
                //_jwtHelperService.TestesProblemaLogin("VerificarToken", "Entrou no if -if (tokenValido)-");

                var conteudo = new JwtSecurityTokenHandler().ReadJwtToken(token.Token);
                var time = long.Parse(conteudo.Claims.Where(x => x.Type == "exp").FirstOrDefault(x => x.Type == "exp").Value);
                DateTime DtExpiramento = DateTimeOffset.FromUnixTimeSeconds(time).DateTime.ToLocalTime();
                if (DateTime.Now < DtExpiramento)
                {
                    //_jwtHelperService.TestesProblemaLogin("VerificarToken", "Entrou no if -if (DateTime.Now < DtExpiramento)-");

                    var claims = conteudo.Claims.Where(x => x.Type == "nomeCompleto" || x.Type == "login" || x.Type == "email" || x.Type == "celula" || x.Type == "uidNumber" || x.Type == "EALogin");
                    var informacoesFormatada = new InformacoesUsuarioViewModel
                    {
                        Email = claims.FirstOrDefault(x => x.Type == "email").Value,
                        NomeCompleto = claims.FirstOrDefault(x => x.Type == "nomeCompleto").Value,
                        Login = claims.FirstOrDefault(x => x.Type == "login").Value,
                        Celula = claims.FirstOrDefault(x => x.Type == "celula").Value != null ? claims.FirstOrDefault(x => x.Type == "celula").Value.Split(' ')[1] : "",
                        UidNumber = claims.FirstOrDefault(x => x.Type == "uidNumber").Value,
                        EALogin = claims.FirstOrDefault(x => x.Type == "EALogin").Value != null ? claims.FirstOrDefault(x => x.Type == "EALogin").Value : ""
                    };
                    return await Task.Run(() => Ok(new { dados = informacoesFormatada, notifications = "", success = true }));

                }
                else
                {
                    return await Task.Run(() => Ok(new { dados = "", notifications = "Token inválido", success = false }));
                }
            }
            else
            {
                return await Task.Run(() => Ok(new { dados = "", notifications = "Token inválido", success = false }));
            }

        }
    }
}
