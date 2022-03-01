namespace CprScanner
{
    using System;

    class ConsoleLogger : ILogger
    {
        public void Error(string message)
        {
            Console.Error.WriteLine(message);
        }

        public void Warning(string message)
        {
            this.Info(message);
        }

        public void Info(string message)
        {
            Console.Out.WriteLine(message);
        }
    }
}