using System.IdentityModel.Tokens.Jwt;

namespace MicrosoftAuth.POC.Backend.Utils.Helper
{
    public interface IJwtHelperService
    {
        string GetUserFromToken(string stream);
        //string GetUserFromToken2(string stream);
        //TokenDataDto GetTokenInformations(string token);
        //void TestesProblemaLogin(string stream, string origem);
        //JwtSecurityToken DecodeToken(string token);
    }
}
