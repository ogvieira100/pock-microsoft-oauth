using MicrosoftAuth.POC.Backend.Utils.Helper;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using static MicrosoftAuth.POC.Backend.Utils.Helper.JwtUtils;

namespace MicrosoftAuth.POC.Backend.Utils.Helper
{
    public class Tokens
    {
        //public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        //{
        //    var response = new
        //    {
        //        login = identity.Claims.Single(c => c.Type == Claims.Strings.JwtClaimIdentifiers.Login).Value,
        //        name = identity.Claims.Single(c => c.Type == Claims.Strings.JwtClaimIdentifiers.NomeCompleto).Value,
        //        auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
        //        expires_in = (int)jwtOptions.ValidFor.TotalSeconds
        //    };

        //    return JsonConvert.SerializeObject(response, serializerSettings);
        //}

        public static async Task<string> GenerateJwtToken(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            var auth_token = await jwtFactory.GenerateEncodedToken(userName, identity);
            return auth_token;
        }

        //public static async Task<string> GenerateAplicacaoJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName)
        //{
        //    return await jwtFactory.GenerateEncodedTokenAplicacao(userName, identity);
        //}

        //public static async Task<string> GenerateJwtLogin(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        //{
        //    return await Task.Run(() => jwtFactory.GenerateEncodedTokenLogin(userName, identity));
        //}

        //public static async Task<bool> CanRenewToken(string token)
        //{
        //    /***
        //    * string valida para base 64
        //    * FromBase64String nao aceita '.' capenas caracteres multiplo de  4
        //    * se a string nao possuir o tamanho correto devera acrescentar um =.
        //    */

        //    int indexOfFirstPoint = token.IndexOf('.') + 1;
        //    String toDecode = token.Substring(indexOfFirstPoint, token.LastIndexOf('.') - indexOfFirstPoint);
        //    while (toDecode.Length % 4 != 0)
        //    {
        //        toDecode += '=';
        //    }

        //    //Decode the string
        //    string decodedString = Encoding.ASCII.GetString(Convert.FromBase64String(toDecode));

        //    //Get the "exp" part of the string
        //    Regex regex = new Regex("(\"exp\":)([0-9]{1,})");
        //    Match match = regex.Match(decodedString);
        //    long timestamp = Convert.ToInt64(match.Groups[2].Value);

        //    DateTime tokenExpDate = new DateTime(1970, 1, 1).AddSeconds(timestamp).ToLocalTime();

        //    //bool permission;
        //    //bool permission = (tokenExpDate.Day == DateTime.Now.Day);
        //    return DateTime.Today.Date.CompareTo(tokenExpDate.Date) == 0;
        //    // int permission = DateTime.Compare(date, compareTo);
        //    //return permission;
        //}
    }
}