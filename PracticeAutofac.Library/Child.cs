using System;

namespace PracticeAutofac.Library
{
    public class Child
    {
        //void Test<T>(T a, bool b)
        //{
        //    var test = a is int? & b;              // does not compile
        //    var test2 = a is Nullable<int> & b;    // does compile
        //}

        public Guid ChildId { get; set; }

        public string Name { get; set; }

        public Parent Parent { get; set; }

        public Child()
        {
            Console.WriteLine("Child being created....");
        }

        public void SetParent(Parent parent)
        {
            Parent = parent;
        }

        public override string ToString() => "Hi there";
    }
}