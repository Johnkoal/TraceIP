using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TraceIP.Web.Models;

namespace TraceIP.Web.Pages
{
    public class SummaryModel : PageModel
    {
        public DetailModel DetailModel { get; set; } = new DetailModel();
        public List<DetailItemModel> DetailItemsModel { get; set; } = new List<DetailItemModel>();
        private readonly AppSettings _appSettings;

        public SummaryModel(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(AppSettings));
        }

        public async Task<IActionResult> OnGet()
        {
            // Se consulta el servicio
            var listResponses = new List<ApiListResponse>();
            using (HttpClient client = new HttpClient())
            {
                var url = $"{_appSettings.UrlApi}traceip/all";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                listResponses = JsonConvert.DeserializeObject<List<ApiListResponse>>(jsonResponse);
            }

            if (listResponses == null || listResponses.Count.Equals(0))
                return Page();

            // Se agrupa por paises
            var countries = listResponses
            .GroupBy(c => c.Country)
            .Select(g => new
            {
                Country = g.Key,
                AverageDistance = (int)Math.Round(g.Average(c => c.DistanceKms), 0),
                Count = g.Count()
            })
            .OrderBy(x => x.Country);

            foreach (var country in countries)
                DetailItemsModel.Add(new DetailItemModel()
                {
                    Country = country.Country,
                    DistanceKms = $"{country.AverageDistance.ToString("N0")} kms",
                    Invocation = country.Count
                });

            // Se obtienen las distancias min y max
            var disMin = listResponses.OrderBy(x => x.DistanceKms).FirstOrDefault();
            var disMax = listResponses.OrderByDescending(x => x.DistanceKms).FirstOrDefault();

            var sumAverage = 0;
            var sumInvocation = 0;
            foreach (var country in countries)
            {
                sumAverage += (country.AverageDistance * country.Count);
                sumInvocation += country.Count;
            }

            DetailModel.DistanceMax = $"{disMax.Country}, '{disMax.Ip}' ({disMax.DistanceKms.ToString("N0")} kms)";
            DetailModel.DistanceMin = $"{disMin.Country}, '{disMin.Ip}' ({disMin.DistanceKms.ToString("N0")} kms)";
            DetailModel.DistanceAverage = $"{(sumAverage / sumInvocation).ToString("N0")} kms";

            return Page();
        }
    }
}
