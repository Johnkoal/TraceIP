using Microsoft.AspNetCore.Mvc;
using TraceIP.Domain.Interfaces;
using TraceIP.Infraestructure.Logger;

namespace TraceIP.Api.Controllers
{
    [Route("api/traceip")]
    [ApiController]
    public class TraceIpController : ControllerBase
    {
        private readonly ILoggerService _loggerService;
        private readonly ITraceIpService _traceIpService;

        public TraceIpController(
            ILoggerService loggerService,
            ITraceIpService traceIpService)
        {
            _loggerService = loggerService ?? throw new ArgumentNullException(nameof(ILoggerService));
            _traceIpService = traceIpService ?? throw new ArgumentNullException(nameof(ITraceIpService));
        }

        /// <summary>
        /// Endpoint: Get IP information
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpGet("{ip}")]
        public async Task<IActionResult> Get(string ip)
        {
            try
            {
                //ip = "186.85.11.5";
                var _res = await _traceIpService.GetIp(ip);

                return Ok(_res);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        // GET api/<TraceIpController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}
    }
}
