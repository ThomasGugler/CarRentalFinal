namespace CarRental.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CarRental.Contracts;
    using CarRental.Models;

    /// <summary>
    /// Datenbank , WCF Service, etc...
    /// </summary>
    public class ReservationService : IReservationService
    {
        public IEnumerable<CarModel> FindAvailableCars(DateTime requestedReservationStartDateTime, DateTime requestedReservationEndDateTime, string cityForRequestedReservation)
        {
            yield return CarModel.CreateObject("VW Golf", "W-12345", 213, "B");
            yield return CarModel.CreateObject("Mercedes C", "W -9999", 213, "D");
        }

        public async Task<IEnumerable<CarModel>> FindAvailableCarsAsync(DateTime requestedReservationStartDateTime, DateTime requestedReservationEndDateTime, string cityForRequestedReservation)
        {
            var result = await Task.Run(() => 
            {
                return new CarModel[]
                {
                    CarModel.CreateObject("VW Golf", "W-12345", 213, "B"),
                    CarModel.CreateObject("Mercedes C", "W -9999", 213, "D")
                };
            });
            return result;
        }

        public bool TakeCarReservervation(CustomerModel customer, DateTime requestedReservationStartDateTime, DateTime requestedReservationEndDateTime, string city)
        {
            if (requestedReservationStartDateTime > requestedReservationEndDateTime)
                throw new InvalidOperationException("requestedReservationStartDateTime darf nicht größer als requestedReservationEndDateTime sein.");
            return true;
        }
    }
}
