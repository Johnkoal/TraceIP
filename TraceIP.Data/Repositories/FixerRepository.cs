using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TraceIP.Data.Entities.Fixer;
using TraceIP.Domain.Entities.Fixer;
using TraceIP.Domain.Interfaces;
using TraceIP.Infraestructure.AppSections;

namespace TraceIP.Data.Repositories
{
    public class FixerRepository : IFixerRepository
    {
        private readonly ExternalServices _externalServices;
        public FixerRepository(IOptions<ExternalServices> options)
        {
            _externalServices = options.Value ?? throw new ArgumentNullException(nameof(ExternalServices));
        } 

        public async Task<Response> FindAsync(string currencyCode)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var url = $"{_externalServices.Fixer_Url}?access_key={_externalServices.Fixer_Key}&symbols={currencyCode}";
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(jsonResponse))
                        return null;

                    var result = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);
                    return new Response()
                    {
                        BaseCurrency = result.BaseCurrency,
                        Rates = result.Rates
                    };
                }
                catch (HttpRequestException e)
                {
                    return null;
                }
            }
        }
    }
}
