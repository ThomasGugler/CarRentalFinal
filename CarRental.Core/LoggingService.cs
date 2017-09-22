namespace CarRental.Core
{
    using System;
    using CarRental.Contracts;

    public class LoggingService : ILoggingService
    {
        public void LogError(string message)
        {
            Console.WriteLine(message);
        }

        public void LogInformation(string message)
        {
            Console.WriteLine(message);
        }

        public void LogException(Exception exception)
        {
            Console.WriteLine(exception.Message);
        }
    }
}
