using System;

namespace PracticeAutofac.Library
{
    public class ConsoleLog : ILog, IConsole, IDisposable
    {
        public ConsoleLog()
        {
            Console.WriteLine("Console Log is Created");

        }
        public void Write(string message)
        {
            Console.WriteLine(message);
        }

        public void Dispose()
        {
            Console.WriteLine("Console Log is no longer needed");
        }
    }
}