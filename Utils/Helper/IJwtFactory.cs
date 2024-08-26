using System.Security.Claims;

namespace MicrosoftAuth.POC.Backend.Utils.Helper
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
        //ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
        ClaimsIdentity GenerateClaimsIdentity(string login, string nomeCompleto, string email);
        //ClaimsIdentity GenerateClaimsIdentity(string login, string nomeCompleto, string email, string password);
        //ClaimsIdentity GenerateClaimsIdentity(string login, string nomeCompleto, string email, string password, string celula);
        //ClaimsIdentity GenerateClaimsIdentity(string login, string nomeCompleto, string email, string password, string celula, string uidNumber, List<int> celulasVisualizadas, string celulaTratada);
        //ClaimsIdentity GenerateClaimsIdentity(string login, string nomeCompleto, string email, string password, string celula, string uidNumber, List<int> celulasVisualizadas, string celulaTratada, string EALogin, string idPessoa = null, string idPais = null);

        //ClaimsIdentity GenerateClaimsIdentity(string login);
        //string GenerateEncodedTokenLogin(string userName, ClaimsIdentity identity);
        //ClaimsIdentity GenerateClaimsIdentityAplicacao(string login);
        //Task<string> GenerateEncodedTokenAplicacao(string userName, ClaimsIdentity identity);
    }
}
