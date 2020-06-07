using System;
using System.Collections.Generic;

namespace PracticeAutofac.Library
{
    public class EnumerationReporting
    {
        private readonly IList<ILogger> _loggers;

        public EnumerationReporting(IList<ILogger> loggers)
        {
            _loggers = loggers;
        }

        public void Report()
        {
            foreach (var logger in _loggers)
            {
                Console.WriteLine($"Hello , this is {logger.GetType().Name}");
            }
        }

    }
}