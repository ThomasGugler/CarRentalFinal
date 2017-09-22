namespace CarRental.BusinessLayer
{
    using CarRental.Models;

    public class CarBCalculation : CarBaseCalculation
    {
        public CarBCalculation(CustomerModel customer) : base(customer, 65)
        {
        }
        protected override decimal GetDiscount()
        {
            switch (this.Customer.CustomerType)
            {
                case CustomerType.ConsumerPremium:
                    {
                        return 0.03m;
                    }

                case CustomerType.BusinessPremium:
                    {
                        return 0.04m;
                    }
            }
            return 0M;
        }
    }
}
