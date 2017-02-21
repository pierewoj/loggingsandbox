using System;
using Microsoft.Extensions.Logging;

namespace LoggingSandbox3
{
    public class MyLogger : ILogger
    {
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var formatted = formatter(state, exception);

        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new DummyDisposable();
        }
    }
}