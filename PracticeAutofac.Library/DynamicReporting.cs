using System;
using Autofac.Features.OwnedInstances;

namespace PracticeAutofac.Library
{
    public class DynamicReporting
    {
        private readonly Func<ConsoleLogger> _consoleLoFunc;

        private readonly Func<string, SmsLogger> _smsLoggerFunc;

        public DynamicReporting(Func<ConsoleLogger> consoleLoFunc, 
            Func<string, SmsLogger> smsLoggerFunc)
        {
            _consoleLoFunc = consoleLoFunc;
            _smsLoggerFunc = smsLoggerFunc;
        }

        public void Report()
        {
            _consoleLoFunc().Write("Reporting to console");
            _consoleLoFunc().Write("And again");

            _smsLoggerFunc("09359167820").Write("Messaging admin....");
        }
    }
}