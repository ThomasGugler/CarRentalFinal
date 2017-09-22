namespace CarRental.BusinessLayer
{
    using CarRental.Models;

    public class CarDCalculation : CarBaseCalculation
    {
        public CarDCalculation(CustomerModel customer) : base(customer, 120)
        {
        }
        protected override decimal GetDiscount()
        {
            switch (this.Customer.CustomerType)
            {
                case CustomerType.ConsumerPremium:
                    {
                        return 0.06m;
                    }

                case CustomerType.Business:
                    {
                        return 0.08m;
                    }

                case CustomerType.BusinessPremium:
                    {
                        return 0.12m;
                    }
            }
            return 0M;
        }
    }
}
