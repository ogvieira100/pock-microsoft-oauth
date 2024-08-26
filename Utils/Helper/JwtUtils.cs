namespace MicrosoftAuth.POC.Backend.Utils.Helper
{
    public class JwtUtils
    {
        public static class Claims
        {
            public static class Strings
            {
                public static class JwtClaimIdentifiers
                {
                    public const string Rol = "rol", Id = "id", Codigo = "codigo", Name = "name", Login = "login", Email = "email", NomeCompleto = "nomeCompleto", Password = "password", Celula = "celula", UidNumber = "uidNumber", CelulasVisualizadas = "celulasVisualizadas", CelulaTratada = "celulaTratada", EALogin = "EALogin", IdPessoa = "idPessoa", IdPais = "idPais";
                }

                public static class JwtClaims
                {
                    public const string ApiAccess = "api_access", valorCodigo = "1234";
                }
            }
        }
    }
}
