using System;
using System.Collections.Generic;
using System.Text;
using CarAdverts.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace CarAdverts.Infrastructure.Logging
{


    public class LoggerAdapter<T> : Logger<T>, IAppLogger<T>
    {
        private readonly ILogger<T> _logger;

        public LoggerAdapter(ILoggerFactory factory) : base(factory)
        {
            _logger = factory.CreateLogger<T>();
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }

        public void LogError(string message, params object[] args)
        {
            _logger.LogError(message, args);
        }
    }
}
