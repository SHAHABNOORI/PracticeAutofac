using System;
using Autofac;

namespace PracticeAutofac.Library
{
    public class MyClass : IStartable
    {
        public MyClass()
        {
            Console.WriteLine("This is MyClass Constructor Message...");
        }
        public void Start()
        {
            Console.WriteLine("Container being built...");
        }
    }
}