namespace PracticeAutofac.Library
{
    public class Service : IService
    {
        private readonly IRepository<Person> _repository;

        public Service(IRepository<Person> repository)
        {
            _repository = repository;
        }

        public void ShowMessage()
        {
            _repository.PrintMessage();
        }
    }
}