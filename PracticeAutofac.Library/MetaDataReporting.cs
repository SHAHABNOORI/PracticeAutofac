using System;
using Autofac.Features.Metadata;

namespace PracticeAutofac.Library
{
    public class MetaDataReporting
    {
        private readonly Meta<ConsoleLogger> _consoleLoggerMeta;

        public MetaDataReporting(Meta<ConsoleLogger> consoleLoggerMeta)
        {
            _consoleLoggerMeta = consoleLoggerMeta;
        }

        public void Report()
        {
            _consoleLoggerMeta.Value.Write("Starting report");

            if ((string)_consoleLoggerMeta.Metadata[$"mode"] == "verbose")
                _consoleLoggerMeta.Value.Write($"Verbose Mode : Logger started on {DateTime.Now}");
        }
    }
}