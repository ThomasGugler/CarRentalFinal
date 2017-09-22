namespace CarRental.Contracts
{
    using System;

    public interface ILoggingService
    {
        void LogInformation(string message);

        void LogError(string message);

        void LogException(Exception exception);
    }
}
