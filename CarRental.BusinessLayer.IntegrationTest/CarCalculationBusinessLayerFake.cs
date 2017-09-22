namespace CarRental.BusinessLayer.IntegrationTest
{
    using System;
    using CarRental.Contracts;
    using CarRental.Models;

    public class CarCalculationBusinessLayerFake : ICarCalculationBusinessLayer
    {
        public decimal CalculatePrice(CustomerModel customer, CarModel car, DateTime requestedReservationStartDateTime, DateTime requestedReservationEndDateTime)
        {
            return 1234M;
        }
    }
}
