using System;

namespace PracticeAutofac.Library
{
    public class AnotherViewModel
    {
        private readonly Entity _entity;

        public AnotherViewModel(Entity entity)
        {
            _entity = entity;
        }

        public void Method()
        {
            Console.WriteLine(_entity);
        }
    }
}