using System;
using LoggingSandbox3.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace LoggingSandbox3
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)

                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IValuesService, ValuesService>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Log.Logger = 
                new LoggerConfiguration().ReadFrom.Configuration(Configuration).
                Enrich.FromLogContext().
                CreateLogger();

            loggerFactory.AddSerilog();
            loggerFactory.AddProvider(new MyProvider());
            app.UseMvc();
        }
    }
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

    public class DummyDisposable : IDisposable
    {
        public void Dispose()
        {

        }
    }
}
