using Microsoft.Extensions.Logging;

namespace LoggingSandbox3.Controllers
{
    public interface IValuesService
    {
        string[] GetValues();
    }

    class ValuesService : IValuesService
    {
        private readonly ILogger<ValuesService> _logger;

        public ValuesService(ILogger<ValuesService> logger)
        {
            _logger = logger;
        }
        public string[] GetValues()
        {
            using (_logger.BeginScope("Serwisowy scope"))
            {
                _logger.LogInformation("Log w serwisie");
                return new[] { "aa", "bb" };
            }
        }
    }
}
