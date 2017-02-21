using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace LoggingSandbox3.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ILogger _logger;
        private readonly IValuesService _valuesService;

        public ValuesController(ILogger<ValuesController> logger, IValuesService valuesService)
        {
            _valuesService = valuesService;
            _logger = logger;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            using(LogContext.PushProperty("PushedProperty", "Ohoho"))
            using (_logger.BeginScope("akcja get w kontrolerze"))
            {
                _logger.LogInformation("Getting values");
                return _valuesService.GetValues();
            }
            
        }
    }
}
