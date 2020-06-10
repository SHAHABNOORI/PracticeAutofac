using System;

namespace PracticeAutofac.Library
{
    public class SingletonResource : IResource
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public SingletonResource()
        {
            Console.WriteLine("Singleton Resource Created");
        }

        public void Dispose()
        {
            Console.WriteLine("Singleton Resource Destroyed");
        }

        public void GuidGenerator(string title)
        {
            Console.WriteLine($"Singleton :: {title} :: {Id}");
        }
    }
}