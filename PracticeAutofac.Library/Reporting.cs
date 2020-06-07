

using System;

namespace PracticeAutofac.Library
{
    public class Reporting
    {
        private Lazy<ConsoleLogger> _logger;

        public Reporting(Lazy<ConsoleLogger> logger)
        {
            _logger = logger;
            Console.WriteLine("Reporting component created");
        }

        public void Report()
        {
            _logger.Value.Write("Logger started");
        }
    }
}