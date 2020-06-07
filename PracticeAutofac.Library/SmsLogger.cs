using System;

namespace PracticeAutofac.Library
{
    public class SmsLogger : ILogger
    {
        private readonly string _phoneNumber;

        public SmsLogger(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
            Console.WriteLine($"Sms Logger created at {DateTime.Now.Ticks}");
        }
        public void Dispose()
        {

        }

        public void Write(string message)
        {
            Console.WriteLine($"Sms to {_phoneNumber} : [{message}]");
        }
    }
}