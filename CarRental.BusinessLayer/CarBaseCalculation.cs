namespace CarRental.BusinessLayer
{
    using System;
    using CarRental.Models;

    public abstract class CarBaseCalculation
    {
        private readonly int faktor;
        private readonly CustomerModel customer;

        public CarBaseCalculation(CustomerModel customer, int faktor)
        {
            if (customer == null) throw new ArgumentNullException(nameof(customer));
            this.faktor = faktor;
            this.customer = customer;
        }

        private CarBaseCalculation()
        {
        }

        public CustomerModel Customer { get { return this.customer; } }

        public decimal CalculatePrice(DateTime requestedReservationStartDateTime, DateTime requestedReservationEndDateTime)
        {
            decimal cp = this.faktor * (requestedReservationEndDateTime - requestedReservationStartDateTime).Days;
            // Discount on price for CustomerModel:
            cp = this.CalculateDiscount(cp);
            return cp;
        }

        protected virtual decimal GetDiscount()
        {
            return 0M;
        }

        private decimal CalculateDiscount(decimal cp)
        {
            var discount = this.GetDiscount();
            if (discount > 0)
                cp = cp - (cp * discount);
            return cp;
        }
    }
}
