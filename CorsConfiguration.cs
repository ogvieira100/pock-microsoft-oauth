namespace MicrosoftAuth.POC.Backend
{
    public static class CorsConfiguration
    {
        public static string CorsPolicyDevelopment { get; set; } = "AllowAll";
        public static string CorsPolicyProduction { get; set; } = "CorsProductionPolicy";
        public static string[] UrlAmbientes { get; set; } = { "http://stfcorp.stefanini.com" };

        public static void AddCorsConfigurationDevelopment(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builderCors =>
                    {
                        builderCors.AllowAnyMethod()
                        .AllowAnyHeader()
                        //.SetIsOriginAllowed(origin => true)
                        .AllowCredentials();
                    });
            });
        }
    }
}
