using System;

namespace PracticeAutofac.Library
{
    public class ViewModel
    {
        private readonly Entity.Factory _factory;

        public ViewModel(Entity.Factory factory)
        {
            _factory = factory;
        }

        public void Method()
        {
            var entity = _factory();
            Console.WriteLine(entity);
        }
    }
}