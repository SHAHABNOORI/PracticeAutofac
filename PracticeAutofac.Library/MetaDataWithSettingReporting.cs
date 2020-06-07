using System;
using Autofac.Features.Metadata;

namespace PracticeAutofac.Library
{
    public class MetaDataWithSettingReporting
    {
        private readonly Meta<ConsoleLogger, Settings> _consoleLoggerMeta;

        public MetaDataWithSettingReporting(Meta<ConsoleLogger, Settings> consoleLoggerMeta)
        {
            _consoleLoggerMeta = consoleLoggerMeta;
        }


        public void Report()
        {
            _consoleLoggerMeta.Value.Write("Starting report");

            if (_consoleLoggerMeta.Metadata.LogMode == "verbose")
                _consoleLoggerMeta.Value.Write($"Verbose Mode : Logger started on {DateTime.Now}");
        }
    }
}