namespace CarRental.Models
{
    using System;

    public static class CustomerFactory
    {
        public static CustomerModel CreateCustomer(CustomerModel customer)
        {
            customer.CustomerNumber = Guid.NewGuid().ToString();
            if (!customer.IsValid)
                throw new InvalidOperationException();
            // TODO : Weitere regelprüfungen bzw. new operatoren hier...
            return customer;
        }
    }
}
