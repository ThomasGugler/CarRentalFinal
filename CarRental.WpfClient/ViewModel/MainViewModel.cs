namespace CarRental.WpfClient.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using CarRental.Contracts;
    using CarRental.Models;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        private readonly ICarRentalBusinessLayer carRentalBusinessLayer;
        private ICommand findAvailableCarsForRentalCommand;
        private ObservableCollection<CarModel> carsList = new ObservableCollection<CarModel>();

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(ICarRentalBusinessLayer carRentalBusinessLayer) 
        {
            this.findAvailableCarsForRentalCommand = new RelayCommand(this.FindCars);
            this.carRentalBusinessLayer = carRentalBusinessLayer;
        }

        public ICommand FindAvailableCarsForRentalCommand
        {
            get => this.findAvailableCarsForRentalCommand;
        }

        public ObservableCollection<CarModel> CarsList
        {
            get => this.carsList;
        }

        public void FindCars()
        {
            this.carsList.Clear();
            var customer = CustomerModel.CreateObject(CustomerType.BusinessPremium, "Thomas", "Gugler", new DateTime(1977, 3, 27));
            var cars = this.carRentalBusinessLayer.FindAvailableCarsForRental(customer, new DateTime(2017, 9, 18), new DateTime(2017, 9, 23), "Vienna");
            foreach (var car in cars.Keys)
                this.carsList.Add(car);
        }
    }
}