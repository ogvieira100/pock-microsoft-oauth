using System.IdentityModel.Tokens.Jwt;

namespace MicrosoftAuth.POC.Backend.Utils.Helper
{
    public class JwtHelperService : IJwtHelperService
    {
        public string GetUserFromToken(string stream)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            //TestesProblemaLogin(stream, "GetUserFromToken");
            var user = tokenS.Claims.FirstOrDefault(x => x.Type == "preferred_username").Value.ToString();
            return user = user.Split("@")[0];
        }

        //public string GetUserFromToken2(string stream)
        //{

        //    var handler = new JwtSecurityTokenHandler();
        //    var jsonToken = handler.ReadToken(stream);
        //    var tokenS = jsonToken as JwtSecurityToken;
        //    //TestesProblemaLogin(stream, "GetUserFromToken2");
        //    var user = tokenS.Claims.FirstOrDefault(x => x.Type == "login").Value.ToString();
        //    return user;
        //}

        //public TokenDataDto GetTokenInformations(string token)
        //{
        //    var handler = new JwtSecurityTokenHandler();
        //    var jsonToken = handler.ReadToken(token);
        //    var tokenS = jsonToken as JwtSecurityToken;
        //    var tokenData = new TokenDataDto
        //    {
        //        Login = tokenS?.Claims.FirstOrDefault(x => x.Type == "login")?.Value,
        //        NomeCompleto = tokenS?.Claims.FirstOrDefault(x => x.Type == "nomeCompleto")?.Value,
        //        Celula = tokenS?.Claims.FirstOrDefault(x => x.Type == "celula")?.Value,
        //        CelulaTratada = tokenS?.Claims.FirstOrDefault(x => x.Type == "celulaTratada")?.Value,
        //        CelulasVisualizadas = ConverteCelulasVisualizadas(tokenS?.Claims.FirstOrDefault(x => x.Type == "celulasVisualizadas")?.Value),
        //        EacessoLogin = tokenS?.Claims.FirstOrDefault(x => x.Type == "EALogin")?.Value,
        //        Email = tokenS?.Claims.FirstOrDefault(x => x.Type == "email")?.Value,
        //        IdEacesso = tokenS?.Claims.FirstOrDefault(x => x.Type == "uidNumber")?.Value,
        //        IdPessoa = tokenS?.Claims.FirstOrDefault(x => x.Type == "idPessoa")?.Value,
        //        IdPais = tokenS?.Claims.FirstOrDefault(x => x.Type == "idPais")?.Value
        //    };
        //    return tokenData;
        //}

        //public JwtSecurityToken DecodeToken(string token)
        //{
        //    var handler = new JwtSecurityTokenHandler();

        //    if (!handler.CanReadToken(token))
        //        return null;

        //    return handler.ReadToken(token) as JwtSecurityToken;
        //}

        //private static List<int> ConverteCelulasVisualizadas(string celulasVisualizadas)
        //{
        //    if (!string.IsNullOrEmpty(celulasVisualizadas))
        //    {
        //        var numbers = celulasVisualizadas.Split(',').Select(int.Parse).ToList();
        //        return numbers;
        //    }
        //    return null;
        //}

        //public void TestesProblemaLogin(string stream, string origem)
        //{
        //    if (stream == "VerificarToken")
        //    {
        //        using (SqlConnection stfcorpConnection = new SqlConnection("Data Source=SOALV3SQLHML01\\HML,1439;Initial Catalog=DBEACESSO_DAILY;User ID=stfcorp;Password=ar3d84s2"))
        //        {

        //            stfcorpConnection.Open();

        //            var sQuery = $@"insert into tblloggenerico values ('ANALISE','VerificarToken','{origem}',GETDATE(),'')";
        //            var result = stfcorpConnection.Query(sQuery);

        //            stfcorpConnection.Close();

        //        }
        //    }
        //    else
        //    {
        //        try
        //        {
        //            var handler = new JwtSecurityTokenHandler();
        //            var jsonToken = handler.ReadToken(stream);
        //            var tokenS = jsonToken as JwtSecurityToken;

        //            foreach (var i in tokenS.Claims.ToList())
        //            {
        //                var type = i.Type.ToString();
        //                var value = i.Value.ToString();

        //                using (SqlConnection stfcorpConnection = new SqlConnection("Data Source=SOALV3SQLHML01\\HML,1439;Initial Catalog=DBEACESSO_DAILY;User ID=stfcorp;Password=ar3d84s2"))
        //                {

        //                    stfcorpConnection.Open();

        //                    var sQuery = $@"insert into tblloggenerico values ('ANALISE','LOGIN-TYPE','{type}',GETDATE(),'{origem}')";
        //                    var result = stfcorpConnection.Query(sQuery);

        //                    var sQuery2 = $@"insert into tblloggenerico values ('ANALISE','LOGIN-VALUE','{value}',GETDATE(),'{origem}')";
        //                    var result2 = stfcorpConnection.Query(sQuery2);

        //                    stfcorpConnection.Close();

        //                }
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            throw new Exception(e.Message);
        //        }
        //    }



        //}
    }
}
