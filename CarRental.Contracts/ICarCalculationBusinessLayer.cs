namespace CarRental.Contracts
{
    using System;
    using CarRental.Models;

    public interface ICarCalculationBusinessLayer
    {
        decimal CalculatePrice(CustomerModel customer, CarModel car, DateTime requestedReservationStartDateTime, DateTime requestedReservationEndDateTime);
    }
}
