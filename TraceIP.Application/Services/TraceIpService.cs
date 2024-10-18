using Microsoft.Extensions.Options;
using TraceIP.Application.Interfaces;
using TraceIP.Domain.Entities;
using TraceIP.Domain.Interfaces;
using TraceIP.Infraestructure.AppSections;
using TraceIP.Infraestructure.Logger;

namespace TraceIP.Application.Services
{
    public class TraceIpService : ITraceIpService
    {
        #region Properties
        private readonly ILoggerService _loggerService;
        private readonly IGeoPluginRepository _geoPluginRepository;
        private readonly IFixerRepository _fixerRepository;
        private readonly IIpApiRepository _ipApiRepository;
        private readonly ITimezonedbRepository _timezonedbRepository;
        private readonly IHaversineService _haversineService;
        private readonly IIpResultRepository _ipResultRepository;
        private readonly AppSettings _appSettings;
        #endregion

        #region Constructor
        public TraceIpService(
            ILoggerService loggerService,
            IGeoPluginRepository geoPluginRepository,
            IFixerRepository fixerRepository,
            IIpApiRepository ipApiRepository,
            ITimezonedbRepository timezonedbRepository,
            IHaversineService haversineService,
            IIpResultRepository ipResultRepository,
            IOptions<AppSettings> appSettings)
        {
            _loggerService = loggerService ?? throw new ArgumentNullException(nameof(ILoggerService));
            _geoPluginRepository = geoPluginRepository ?? throw new ArgumentNullException(nameof(IGeoPluginRepository));
            _fixerRepository = fixerRepository ?? throw new ArgumentNullException(nameof(IFixerRepository));
            _ipApiRepository = ipApiRepository ?? throw new ArgumentNullException(nameof(IIpApiRepository));
            _timezonedbRepository = timezonedbRepository ?? throw new ArgumentNullException(nameof(ITimezonedbRepository));
            _haversineService = haversineService ?? throw new ArgumentNullException(nameof(IHaversineService));
            _ipResultRepository = ipResultRepository ?? throw new ArgumentNullException(nameof(IIpResultRepository));
            _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(AppSettings));
        }
        #endregion

        #region Public Methods
        public void Delete()
        {
            _ipResultRepository.DeleteAll();
        }

        public IEnumerable<IpResult> GetAll()
        {
            return _ipResultRepository.GetAll();
        }

        public async Task<IpResponse> GetIp(string ip)
        {
            // Consulta de todos los servicios requeridos
            var _responseIpApi = await _ipApiRepository.FindAsync(ip);
            var _responseCountry = await _geoPluginRepository.FindAsync(ip);
            var _responseFixer = await _fixerRepository.FindAsync(_responseCountry.CurrencyCode);
            var _responseTimezonedb = await _timezonedbRepository.FindAsync(_responseIpApi.CountryCode);

            // Se obtienen las horas y zonas horarias
            DateTime dateTime;
            var textTime = string.Empty;
            var hours = 0;
            foreach (var item in _responseTimezonedb.Zones.DistinctBy(x => x.GmtOffset))
            {
                dateTime = DateTimeOffset.FromUnixTimeSeconds(item.Timestamp).DateTime;
                hours = item.GmtOffset / 3600;
                textTime += $"{dateTime.ToString("hh:mm:ss")} (UTC{(hours >= 0 ? "+" : "")}{hours}), ";
            }

            // Se obtiene la lista de idiomas
            var textlanguage = string.Empty;
            foreach (var item in _responseIpApi.Languages)
                textlanguage += $"{item}, ";

            // Se calcula la distacia
            var disance = _haversineService.CalculateDistance(
                _appSettings.Latitude_Local,
                _appSettings.Longitude_Local,
                _responseIpApi.Latitude,
                _responseIpApi.Longitude);

            var ipResponse = new IpResponse()
            {
                Ip = ip,
                CountryTime = textTime.TrimEnd(' ', ','),
                Country = _responseIpApi.Country,
                City = _responseIpApi.City,
                IsoCode = _responseIpApi.CountryCode,
                Language = textlanguage.TrimEnd(' ', ','),
                CurrencyCode = _responseCountry.CurrencyCode,
                Currency = $"{_responseCountry.CurrencyCode} (1 {_responseCountry.CurrencyCode} = {Math.Round((1 / _responseFixer.Rates[_responseCountry.CurrencyCode]), 5)} EUR)",
                CurrentDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"),
                DistanceKms = $"{disance.ToString()} kms de (Lat {(int)Math.Round(_responseIpApi.Latitude)} Lon {(int)Math.Round(_responseIpApi.Latitude)}) a (Lat {(int)Math.Round(_appSettings.Latitude_Local)} Lon {(int)Math.Round(_appSettings.Longitude_Local)})"
            };

            // Se almacena el registro en la base
            _ipResultRepository.Add(new IpResult()
            {
                Country = ipResponse.Country,
                DistanceKms = disance,
                Ip = ipResponse.Ip
            });

            return ipResponse;
        }
        #endregion
    }
}
