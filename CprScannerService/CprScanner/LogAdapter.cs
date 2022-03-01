namespace CprScanner
{
    using System;
    using CprScanner.Business.Ports;

    class LogAdapter : IBusinessLogger
    {
        private readonly ILogger logger;

        public LogAdapter(ILogger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Error(string message) => this.logger.Error(message);
        public void Warning(string message) => this.logger.Warning(message);
        public void Info(string message) => this.logger.Info(message);
    }
}
