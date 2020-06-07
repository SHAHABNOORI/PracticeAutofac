using System;

namespace PracticeAutofac.Library
{
    public class Entity
    {
        public delegate Entity Factory();

        private static Random _random = new Random();
        private int number;

        public Entity()
        {
            number = _random.Next();
        }

        public override string ToString()
        {
            return $"Test {number}"; 
        }
    }
}