namespace CarRental.Contracts
{
    using System;
    using System.Collections.Generic;
    using CarRental.Models;

    public interface ICarRentalBusinessLayer
    {
        void CreateNewCustomer(string firstName, string lastName, DateTime dateOfBirth, string street, string city, string postcode, CustomerType customerType);

        Dictionary<CarModel, decimal> FindAvailableCarsForRental(CustomerModel customer, DateTime requestedReservationStartDateTime, DateTime requestedReservationEndDateTime, string city);

        bool CreateCarReservation(CustomerModel customer, DateTime requestedReservationStartDateTime, DateTime requestedReservationEndDateTime, string city);
    }
}
