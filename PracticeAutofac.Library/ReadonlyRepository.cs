using System;

namespace PracticeAutofac.Library
{
    public class ReadonlyRepository<T> : IRepository<T>
    {
        public void PrintMessage()
        {
            Console.WriteLine($"ReadOnly Repository :: {typeof(T)}");
        }
    }
}