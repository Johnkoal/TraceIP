using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TraceIP.Data.Entities.IpApi;
using TraceIP.Domain.Entities.IpApi;
using TraceIP.Domain.Interfaces;
using TraceIP.Infraestructure.AppSections;
using TraceIP.Infraestructure.Exceptions;

namespace TraceIP.Data.Repositories
{
    public class IpApiRepository : IIpApiRepository
    {
        private readonly ExternalServices _externalServices;
        public IpApiRepository(IOptions<ExternalServices> externalServices)
        {
            _externalServices = externalServices.Value ?? throw new ArgumentNullException(nameof(ExternalServices));
        }

        public async Task<Response> FindAsync(string ip)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var url = $"{_externalServices.IpApi_Url}{ip}?access_key={_externalServices.IpApi_Key}";
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

                    var _languages = new List<string>();
                    foreach (var item in result.Location.Languages)
                        _languages.Add($"{item.Name} ({item.Code})");

                    return new Response()
                    {
                        CountryCode = result.Country_code,
                        Country = result.Country_name,
                        City = result.City,
                        Languages = _languages,
                        Latitude = result.Latitude,
                        Longitude = result.Longitude
                    };
                }
                catch (HttpRequestException ex)
                {
                    throw new ExceptionExternalService("Error al consultar el servicio IpApi", ex);
                }
            }
        }
    }
}
