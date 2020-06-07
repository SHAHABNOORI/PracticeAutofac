using System;

namespace PracticeAutofac.Library
{
    public class Sample
    {
        private readonly ILog _log;
        private readonly int _id;
        private readonly string _firstName;
        private readonly string _lastName;

        public Sample()
        {
            Console.WriteLine("With 0 param");

        }

        public Sample(ILog log)
        {
            _log = log;
            Console.WriteLine("With log param");
        }

        public Sample(int id)
        {
            _id = id;
            Console.WriteLine("With 1 param");

        }

        public Sample(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
            Console.WriteLine("With 2 param");

        }

        public Sample(int id,string firstName,string lastName)
        {
            _id = id;
            _firstName = firstName;
            _lastName = lastName;
            Console.WriteLine("With 3 param");
        }

    }
}