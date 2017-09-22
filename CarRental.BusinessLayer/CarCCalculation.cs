namespace CarRental.BusinessLayer
{
    using CarRental.Models;

    public class CarCCalculation : CarBaseCalculation
    {
        public CarCCalculation(CustomerModel customer) : base(customer, 90)
        {
        }
        protected override decimal GetDiscount()
        {
            switch (this.Customer.CustomerType)
            {
                case CustomerType.ConsumerPremium:
                    {
                        return 0.05m;
                    }

                case CustomerType.Business:
                    {
                        return 0.06m;
                    }

                case CustomerType.BusinessPremium:
                    {
                        return 0.08m;
                    }
            }
            return 0M;
        }
    }
}
