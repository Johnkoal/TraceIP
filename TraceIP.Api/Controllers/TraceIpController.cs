using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using TraceIP.Api.Entities;
using TraceIP.Domain.Interfaces;
using TraceIP.Infraestructure.Exceptions;
using TraceIP.Infraestructure.Logger;

namespace TraceIP.Api.Controllers
{
    [Route("api/traceip")]
    [ApiController]
    public class TraceIpController : ControllerBase
    {
        #region Properties
        private readonly ILoggerService _loggerService;
        private readonly ITraceIpService _traceIpService;
        #endregion

        #region Constructor
        public TraceIpController(
            ILoggerService loggerService,
            ITraceIpService traceIpService)
        {
            _loggerService = loggerService ?? throw new ArgumentNullException(nameof(ILoggerService));
            _traceIpService = traceIpService ?? throw new ArgumentNullException(nameof(ITraceIpService));
        }
        #endregion

        #region Endpoints
        /// <summary>
        /// Endpoint: Obtiene información de una IP
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpGet("{ip}")]
        public async Task<IActionResult> Get(string ip)
        {
            var _guid = Guid.NewGuid();
            //_loggerService.Trace($"{_guid} - Request ip: {ip}");
            try
            {
                // Se valida que la IP ingresada sea válida
                string pattern = @"\b(?:(?:2[0-5]{2}|1?\d{1,2})\.){3}(?:2[0-5]{2}|1?\d{1,2})\b";
                Regex regex = new Regex(pattern);
                if (!regex.IsMatch(ip))
                    throw new ExceptionInfoRequire($"{ip} no es una dirección IP válida.");

                var ipResponse = await _traceIpService.GetIp(ip);
                return Ok();
                //var traceIpResponse = new TraceIpResponse()
                //{
                //    MessageCode = 1,
                //    Message = "Ejeccción exitosa",
                //    Ip = ipResponse.Ip,
                //    CurrentDate = ipResponse.CurrentDate,
                //    Country = ipResponse.Country,
                //    CountryTime = ipResponse.CountryTime,
                //    Currency = ipResponse.Currency,
                //    CurrencyCode = ipResponse.CurrencyCode,
                //    DistanceKms = ipResponse.DistanceKms,
                //    IsoCode = ipResponse.IsoCode,
                //    Language = ipResponse.Language
                //};

                //_loggerService.Trace($"{_guid} - Response: {JsonConvert.SerializeObject(traceIpResponse)}");
                //return Ok(traceIpResponse);
            }
            catch (Exception ex)
            {
                var traceIpResponse = new TraceIpResponse() { Message = ex.Message };
                
                switch (ex)
                {
                    case ExceptionInfoRequire:
                        traceIpResponse.MessageCode = 2;
                        break;
                    case ExceptionExternalService:
                        traceIpResponse.MessageCode = 3;
                        break;
                    default:
                        traceIpResponse.MessageCode = 10;
                        traceIpResponse.Message = "Error no controlado";
                        break;
                }

                _loggerService.Error($"{_guid} - Error: {ex.Message} ", ex);
                return Ok(traceIpResponse);
            }
        }

        /// <summary>
        /// Endpoint: Consulta todos los registros de IP's
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            try
            {
                var ipResults = _traceIpService.GetAll();
                return Ok(ipResults);
            }
            catch (Exception ex)
            {
                var traceIpResponse = new TraceIpResponse() { Message = ex.Message };

                switch (ex)
                {
                    case ExceptionDatabase:
                        traceIpResponse.MessageCode = 4;
                        break;
                    default:
                        traceIpResponse.MessageCode = 10;
                        traceIpResponse.Message = "Error no controlado";
                        break;
                }

                _loggerService.Error($"Error: {ex.Message} ", ex);
                return Ok(traceIpResponse);
            }
        }

        /// <summary>
        /// Endpoint: Elimina todos los registros
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpDelete]
        public IActionResult Delete()
        {
            try
            {
                _traceIpService.Delete();
                return Ok();
            }
            catch (Exception ex)
            {
                var traceIpResponse = new TraceIpResponse() { Message = ex.Message };

                switch (ex)
                {
                    case ExceptionDatabase:
                        traceIpResponse.MessageCode = 4;
                        break;
                    default:
                        traceIpResponse.MessageCode = 10;
                        traceIpResponse.Message = "Error no controlado";
                        break;
                }

                _loggerService.Error($"Error: {ex.Message} ", ex);
                return Ok(traceIpResponse);
            }
        }
        #endregion
    }
}
