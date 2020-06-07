using System;

namespace PracticeAutofac.Library
{
    public class ConsoleLogger : ILogger
    {
        public ConsoleLogger()
        {
            Console.WriteLine($"Console Logger created at {DateTime.Now.Ticks}");
        }
        public void Dispose()
        {
            Console.WriteLine("Console Logger , no longer Required");
        }

        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}