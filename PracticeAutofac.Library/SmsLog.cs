using System;

namespace PracticeAutofac.Library
{
    public class SmsLog : ILog
    {
        private readonly string _phoneNumber;

        public SmsLog(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }

        public void Write(string message)
        {
            Console.WriteLine($"Sms to {_phoneNumber} : [{message}]");
        }
    }
}