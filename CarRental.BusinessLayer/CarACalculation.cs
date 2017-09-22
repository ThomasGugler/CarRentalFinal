namespace CarRental.BusinessLayer
{
    using CarRental.Models;

    public class CarACalculation : CarBaseCalculation
    {
        public CarACalculation(CustomerModel customer) : base(customer, 50)
        {
        }

        protected override decimal GetDiscount()
        {
            switch (this.Customer.CustomerType)
            {
                case CustomerType.ConsumerPremium:
                    {
                        return 0.02m;
                    }
            }
            return 0M;
        }
    }
}
