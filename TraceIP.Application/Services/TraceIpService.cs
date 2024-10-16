using Microsoft.Extensions.Options;
using TraceIP.Domain.Entities;
using TraceIP.Domain.Interfaces;
using TraceIP.Infraestructure.AppSections;
using TraceIP.Infraestructure.Logger;

namespace TraceIP.Application.Services
{
    public class TraceIpService : ITraceIpService
    {
        private readonly ILoggerService _loggerService;
        private readonly IGeoPluginRepository _geoPluginRepository;
        private readonly IFixerRepository _fixerRepository;
        private readonly IIpApiRepository _ipApiRepository;
        private readonly ITimezonedbRepository _timezonedbRepository;
        private readonly AppSettings _appSettings;

        public TraceIpService(
            ILoggerService loggerService,
            IGeoPluginRepository geoPluginRepository,
            IFixerRepository fixerRepository,
            IIpApiRepository ipApiRepository,
            ITimezonedbRepository timezonedbRepository,
            IOptions<AppSettings> appSettings)
        {
            _loggerService = loggerService ?? throw new ArgumentNullException(nameof(ILoggerService));
            _geoPluginRepository = geoPluginRepository ?? throw new ArgumentNullException(nameof(IGeoPluginRepository));
            _fixerRepository = fixerRepository ?? throw new ArgumentNullException(nameof(IFixerRepository));
            _ipApiRepository = ipApiRepository ?? throw new ArgumentNullException(nameof(IIpApiRepository));
            _timezonedbRepository = timezonedbRepository ?? throw new ArgumentNullException(nameof(ITimezonedbRepository));
            _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(AppSettings));
        }

        public async Task<IpResponse> GetIp(string ip)
        {

            // S1: Get info IP
            var _responseIpApi = await _ipApiRepository.FindAsync(ip);            

            // S2: Get info the country
            var _responseCountry = await _geoPluginRepository.FindAsync(ip);

            // S3: Get info currency
            var _responseFixer = await _fixerRepository.FindAsync(_responseCountry.CurrencyCode);

            // S4: Get info Timezone
            var _responseTimezonedb = await _timezonedbRepository.FindAsync(_responseIpApi.CountryCode);

            // Se calcula la hora y zona horaria
            DateTime dateTime;
            var textTime = string.Empty;
            var hours = 0;
            foreach (var item in _responseTimezonedb.Zones)
            {
                dateTime = DateTimeOffset.FromUnixTimeSeconds(item.Timestamp).DateTime;
                hours = item.GmtOffset / 3600;
                textTime += $"{dateTime.ToString("hh:mm:ss")} (UTC{(hours >= 0 ? "+" : "")}{hours}) ";
            }

            // Se calcula la distacia
            var disance = Haversine(_appSettings.Latitude, _appSettings.Longitude, _responseIpApi.Latitude, _responseIpApi.Longitude);



            var ipResponse = new IpResponse()
            {
                Ip = ip,
                CountryTime = textTime,
                Country = _responseIpApi.Country,
                IsoCode = "",
                Language = _responseIpApi.Language,
                CurrencyCode = _responseCountry.CurrencyCode,
                Currency = _responseFixer.Rates[_responseCountry.CurrencyCode].ToString(),
                CurrentDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"),
                Distance = disance.ToString()
            };

            return ipResponse;
        }

        public static int Haversine(double lat1, double lon1, double lat2, double lon2)
        {
            // Convertir grados a radianes
            double lat1Rad = DegreesToRadians(lat1);
            double lon1Rad = DegreesToRadians(lon1);
            double lat2Rad = DegreesToRadians(lat2);
            double lon2Rad = DegreesToRadians(lon2);

            // Diferencias entre latitudes y longitudes
            double deltaLat = lat2Rad - lat1Rad;
            double deltaLon = lon2Rad - lon1Rad;

            // Fórmula de Haversine
            double a = Math.Pow(Math.Sin(deltaLat / 2), 2) +
                       Math.Cos(lat1Rad) * Math.Cos(lat2Rad) * Math.Pow(Math.Sin(deltaLon / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            // Distancia final
            double distance = 6371 * c;
            return Convert.ToInt32(distance);
        }

        // Método para convertir grados a radianes
        public static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }


    }
}
