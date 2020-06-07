using System;

namespace PracticeAutofac.Library
{
    public class EmailLog : ILog
    {
        private const string AdminEmail = "admin@foo.com";

        public void Write(string message)
        {
            Console.WriteLine($"Email sent to {AdminEmail} : {message}");
        }
    }
}