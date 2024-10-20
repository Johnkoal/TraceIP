using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TraceIP.Web.Models;

namespace TraceIP.Web.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IPRequestModel IPRequest { get; set; } = new IPRequestModel();
        private readonly AppSettings _appSettings; 

        public IndexModel(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(AppSettings));
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.First(x => x.Key == "IPRequest.Search").Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid)
            {
                // Consulta del servicio
                var ipResponse = new ApiIpResponse();
                using (HttpClient client = new HttpClient())
                {
                    var url = $"{_appSettings.UrlApi}traceip/{IPRequest.Search}";
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    ipResponse = JsonConvert.DeserializeObject<ApiIpResponse>(jsonResponse);
                }

                // Si la consulta no es correcta, muestra el mensaje de error entreado . el servicio
                if (ipResponse.Message_code != 1)
                {
                    ViewData["Message"] = ipResponse.Message;
                    return Page();
                }

                // Creación de tabla para visualizar los datos
                IPRequest.Ip = ipResponse.Ip;
                IPRequest.Current_date = ipResponse.Current_date;
                IPRequest.Country = ipResponse.Country;
                IPRequest.City = ipResponse.City;
                IPRequest.Iso_code = ipResponse.Iso_code;
                IPRequest.Language = ipResponse.Language;
                IPRequest.Currency = ipResponse.Currency;
                IPRequest.Country_date = ipResponse.Country_date;
                IPRequest.Distance = $"{ipResponse.Distance}";

                return Page();
            }

            return Page();
        }
    }
}
