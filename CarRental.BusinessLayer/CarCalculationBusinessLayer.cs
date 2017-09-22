namespace CarRental.BusinessLayer
{
    using System;
    using CarRental.Contracts;
    using CarRental.Models;

    public class CarCalculationBusinessLayer : ICarCalculationBusinessLayer
    {
        private readonly ILoggingService loggingService;
        private readonly IContainerService containerService;

        public CarCalculationBusinessLayer(IContainerService containerService, ILoggingService loggingService)
        {
            this.loggingService = loggingService;
            this.containerService = containerService;
        }

        public decimal CalculatePrice(CustomerModel customer, CarModel availableCar, DateTime requestedReservationStartDateTime, DateTime requestedReservationEndDateTime)
        {
            CarBaseCalculation carCalculation;
            switch (availableCar.Category)
            {
                case "A":
                    carCalculation = new CarACalculation(customer);
                    break;
                case "B":
                    carCalculation = new CarBCalculation(customer);
                    break;
                case "C":
                    carCalculation = new CarCCalculation(customer);
                    break;
                case "D":
                    carCalculation = new CarDCalculation(customer);
                    break;
                default:
                    {
                        throw new InvalidOperationException();
                    }
            }
            return carCalculation.CalculatePrice(requestedReservationStartDateTime, requestedReservationEndDateTime);
        }
    }
}
