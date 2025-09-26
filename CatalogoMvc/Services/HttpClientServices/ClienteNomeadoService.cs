using System.Net.Http.Headers;

namespace CatalogoMvc.Services.HttpClientServices;

public static class ClienteNomeadoService
{
    public static IServiceCollection AddClienteNomeadoService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("CatalogoApi", c =>
        {
            var baseAdress = configuration["ServiceUri:CatalogoApi"];
            c.BaseAddress = new Uri(baseAdress);
        });

        services.AddHttpClient("AutenticaApi", c =>
        {
            var baseAdress = configuration["ServiceUri:AutenticaApi"];
            c.BaseAddress = new Uri(baseAdress);
        });

        return services;
    }
}
