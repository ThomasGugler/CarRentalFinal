namespace CarRental.Console
{
    using System;
    using CarRental.BusinessLayer;
    using CarRental.Contracts;
    using CarRental.Core;
    using CarRental.Models;
    using CarRental.Services;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    public class Program
    {
        public static void Main(string[] args)
        {
            var container = new WindsorContainer();
            var containerServce = new ContainerService(container);
            // container, common,..
            container.Register(Component.For<IContainerService>().Instance(containerServce));
            container.Register(Component.For<ILoggingService>().ImplementedBy<LoggingService>());
            // Services (wcf, db...)
            container.Register(Component.For<IReservationService>().ImplementedBy<ReservationService>());
            // Domain layer
            container.Register(Component.For<ICarCalculationBusinessLayer>().ImplementedBy<CarCalculationBusinessLayer>());
            container.Register(Component.For<ICarRentalBusinessLayer>().ImplementedBy<CarRentalBusinessLayer>());
            // Demo:
            var carRental = container.Resolve<ICarRentalBusinessLayer>();
            var customer = CustomerModel.CreateObject(CustomerType.BusinessPremium, "Thomas", "Gugler", new DateTime(1977, 3, 27));
            var cars = carRental.FindAvailableCarsForRental(customer, new DateTime(2017, 9, 18), new DateTime(2017, 9, 23), "Vienna");
        }
    }
}
