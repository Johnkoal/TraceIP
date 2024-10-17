using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TraceIP.Data.Entities.GeoPlugin;
using TraceIP.Domain.Entities.GeoPlugin;
using TraceIP.Domain.Interfaces;
using TraceIP.Infraestructure.AppSections;
using TraceIP.Infraestructure.Exceptions;

namespace TraceIP.Data.Repositories
{
    public class GeoPluginRepository : IGeoPluginRepository
    {
        private readonly ExternalServices _externalServices;
        public GeoPluginRepository(IOptions<ExternalServices> options)
        {
            _externalServices = options.Value ?? throw new ArgumentNullException(nameof(ExternalServices));
        }

        public async Task<Response> FindAsync(string ip)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var url = $"{_externalServices.GeoPlugin_Url}?ip={ip}";
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

                    return new Response()
                    {
                        CountryCode = result.Geoplugin_countryCode,
                        CurrencyCode = result.Geoplugin_currencyCode,
                        CurrencySymbol = result.Geoplugin_currencySymbol,
                        Timezone = result.Geoplugin_timezone
                    };
                }
                catch (HttpRequestException ex)
                {
                    throw new ExceptionExternalService("Error al consultar el servicio GeoPlugin", ex);
                }
            }
        }
    }
}
