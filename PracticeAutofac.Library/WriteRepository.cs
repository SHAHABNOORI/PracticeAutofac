using System;

namespace PracticeAutofac.Library
{
    public class WriteRepository<T> : IRepository<T>
    {
        public void PrintMessage()
        {
            Console.WriteLine($"Write Repository :: {typeof(T)}");
        }
    }
}