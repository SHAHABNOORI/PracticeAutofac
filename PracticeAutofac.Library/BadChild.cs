using System;

namespace PracticeAutofac.Library
{
    public class BadChild : Child
    {
        public Guid ChildId { get; set; }

        public string Name { get; set; }

        public Parent Parent { get; set; }

        public BadChild()
        {
            Console.WriteLine("Bad Child being created....");
        }

        public void SetParent(Parent parent)
        {
            Parent = parent;
        }

        public override string ToString() => "I hate you";
    }
}