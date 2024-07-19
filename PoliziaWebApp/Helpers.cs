using PoliziaWebApp.Interfaces;
using PoliziaWebApp.Services;

namespace PoliziaWebApp
{
    public static class Helpers
    {
        public static IServiceCollection RegisterDAOs(this IServiceCollection services)
        {
            return services
                .AddScoped<IAnagraficaService, AnagraficaService>()
                .AddScoped<IVerbaleService, VerbaleService>()
                .AddScoped<IViolazioneService, ViolazioneService>()
                ;
        }
    }
}
