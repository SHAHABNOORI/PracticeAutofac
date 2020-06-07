using System;
using Autofac.Features.OwnedInstances;

namespace PracticeAutofac.Library
{
    public class OwnedReporting
    {
        private Owned<ConsoleLogger> _loggerOwned;

        public OwnedReporting(Owned<ConsoleLogger> loggerOwned)
        {
            _loggerOwned = loggerOwned;
            Console.WriteLine("Reporting initialized");
        }

        public void ReportOnce()
        {
            _loggerOwned.Value.Write("Log started");
            _loggerOwned.Dispose();
        }
    }
}