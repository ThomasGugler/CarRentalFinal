namespace CarRental.BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using CarRental.Contracts;
    using CarRental.Core;
    using CarRental.Models;

    public class CarRentalBusinessLayer : ICarRentalBusinessLayer, ICarCalculationBusinessLayer
    {
        private readonly ILoggingService loggingService;
        private readonly IContainerService containerService;
        private readonly ICarCalculationBusinessLayer carCalculationBusinessLayer;

        public CarRentalBusinessLayer(IContainerService containerService, ILoggingService loggingService, ICarCalculationBusinessLayer carCalculationBusinessLayer)
        {
            this.loggingService = loggingService;
            this.containerService = containerService;
            this.carCalculationBusinessLayer = carCalculationBusinessLayer;
        }

        public decimal CalculatePrice(CustomerModel customer, CarModel car, DateTime requestedReservationStartDateTime, DateTime requestedReservationEndDateTime)
        {
            return this.carCalculationBusinessLayer.CalculatePrice(customer, car, requestedReservationStartDateTime, requestedReservationEndDateTime);
        }

        public bool CreateCarReservation(CustomerModel customer, DateTime requestedReservationStartDateTime, DateTime requestedReservationEndDateTime, string city)
        {
            try
            {
                if (DateTimeService.Now > requestedReservationStartDateTime)
                    return false;
                var reservationService = this.containerService.Resolve<IReservationService>();
                return reservationService.TakeCarReservervation(customer, requestedReservationStartDateTime, requestedReservationEndDateTime, city);
            }
            catch (InvalidOperationException e)
            {
                this.loggingService.LogError(e.Message);
                return false;
            }
        }

        public void CreateNewCustomer(string firstName, string lastName, DateTime dateOfBirth, string street, string city, string postcode, CustomerType customerType)
        {
            var newCustomer = CustomerModel.CreateObject(customerType, firstName, lastName, dateOfBirth);
            CustomerModel.SetAddress(newCustomer, street, city, postcode);
            var customerService = this.containerService.Resolve<ICustomerService>();
            customerService.AddCustomer(newCustomer);
        }

        public Dictionary<CarModel, decimal> FindAvailableCarsForRental(CustomerModel customer, DateTime requestedReservationStartDateTime, DateTime requestedReservationEndDateTime, string city)
        {
            var reservationService = this.containerService.Resolve<IReservationService>();
            var availableCars = reservationService.FindAvailableCars(requestedReservationStartDateTime, requestedReservationEndDateTime, city);
            decimal cp = 0;
            var result = new Dictionary<CarModel, decimal>();

            foreach (var availableCar in availableCars)
            {
                cp = this.CalculatePrice(customer, availableCar, requestedReservationStartDateTime, requestedReservationEndDateTime);
                result.Add(availableCar, cp);
            }
        
            return result.Count > 0 ? result : null;
        }
    }
}
