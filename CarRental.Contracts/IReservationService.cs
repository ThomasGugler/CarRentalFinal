namespace CarRental.Contracts
{
    using System;
    using System.Collections.Generic;
    using CarRental.Models;

    public interface IReservationService
    {
        IEnumerable<CarModel> FindAvailableCars(DateTime requestedReservationStartDateTime, DateTime requestedReservationEndDateTime, string cityForRequestedReservation);

        bool TakeCarReservervation(CustomerModel customer, DateTime requestedReservationStartDateTime, DateTime requestedReservationEndDateTime, string city);
    }
}
