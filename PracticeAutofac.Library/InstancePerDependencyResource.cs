using System;

namespace PracticeAutofac.Library
{
    public class InstancePerDependencyResource : IResource, IDisposable
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public InstancePerDependencyResource()
        {
            Console.WriteLine("Instance Per Dependency Resource Created");
        }

        public void Dispose()
        {
            Console.WriteLine("Instance Per Dependency Resource Destroyed");
        }


        public void GuidGenerator(string title)
        {
            Console.WriteLine($"InstancePerDependencyResource ::{title} :: {Id}");
        }
    }
}