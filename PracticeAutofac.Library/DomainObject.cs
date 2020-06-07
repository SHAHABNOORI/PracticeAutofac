namespace PracticeAutofac.Library
{
    public class DomainObject
    {
        private readonly SomeService _service;
        private readonly int _value;

        public delegate DomainObject Factory(int value);
        public DomainObject(SomeService service, int value)
        {
            _service = service;
            _value = value;
        }

        public override string ToString()
        {
            return _service.DoSomething(_value);
        }
    }
}