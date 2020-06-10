using Autofac.Features.Indexed;

namespace PracticeAutofac.Library
{
    public class KeyedServiceReporting
    {
        private readonly IIndex<string, ILogger> _loggers;

        public KeyedServiceReporting(IIndex<string, ILogger> loggers)
        {
            _loggers = loggers;
        }

        public void Report()
        {
            _loggers["sms"].Write("Starting report output");
        }
    }
}