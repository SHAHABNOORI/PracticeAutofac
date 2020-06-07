using System;

namespace PracticeAutofac.Library
{
    public interface ILogger : IDisposable
    {
        void Write(string message);
    }
}