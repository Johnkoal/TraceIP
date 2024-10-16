using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TraceIP.Data.Entities.Timezonedb;
using TraceIP.Domain.Entities.Timezonedb;
using TraceIP.Domain.Interfaces;
using TraceIP.Infraestructure.AppSections;
using Zone = TraceIP.Domain.Entities.Timezonedb.Zone;

namespace TraceIP.Data.Repositories
{
    public class TimezonedbRepository : ITimezonedbRepository
    {
        private readonly ExternalServices _externalServices;
        public TimezonedbRepository(IOptions<ExternalServices> options)
        {
            _externalServices = options.Value ?? throw new ArgumentNullException(nameof(ExternalServices));
        }

        public async Task<Response> FindAsync(string countryCode)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var url = $"{_externalServices.Timezonedb_Url}?key={_externalServices.Timezonedb_Key}&format=json&country={countryCode}";
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(jsonResponse))
                        return null;

                    var result = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);
                    var zones = new List<Zone>();

                    foreach (var item in result.Zones)
                    {
                        zones.Add(new Zone 
                        {
                            CountryCode = item.CountryCode,
                            CountryName = item.CountryName,
                            ZoneName = item.ZoneName,
                            GmtOffset = item.GmtOffset,
                            Timestamp = item.Timestamp
                        });
                    }

                    return new Response() { Zones = zones };
                }
                catch (HttpRequestException e)
                {
                    return null;
                }
            }
        }
    }
}
