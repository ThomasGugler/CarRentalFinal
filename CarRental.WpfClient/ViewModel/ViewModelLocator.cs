namespace CarRental.WpfClient.ViewModel
{
    using CarRental.BusinessLayer;
    using CarRental.Contracts;
    using CarRental.Core;
    using CarRental.Services;
    using GalaSoft.MvvmLight.Ioc;
    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
        }

        public static void RegisterAll()
        {
            SimpleIoc.Default.Register<IContainerService, ContainerService>();
            SimpleIoc.Default.Register<ILoggingService, LoggingService>();
            SimpleIoc.Default.Register<ICarCalculationBusinessLayer, CarCalculationBusinessLayer>();
            SimpleIoc.Default.Register<ICarRentalBusinessLayer, CarRentalBusinessLayer>();
            SimpleIoc.Default.Register<IMainViewModel, MainViewModel>();
            // Later..
            SimpleIoc.Default.Register<IReservationService, ReservationService>();
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}