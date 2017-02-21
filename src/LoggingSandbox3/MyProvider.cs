using Microsoft.Extensions.Logging;

namespace LoggingSandbox3
{
    public class MyProvider : ILoggerProvider
    {
        public void Dispose()
        {

        }

        public ILogger CreateLogger(string categoryName)
        {
            return new MyLogger();
        }
    }
}