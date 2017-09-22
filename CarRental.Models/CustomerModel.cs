namespace CarRental.Models
{
    using System;

    public class CustomerModel
    {
        private CustomerModel()
        {
        }

        public int CustomerId { get; }
        public string CustomerNumber { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public CustomerType CustomerType { get; private set; }
        public AddressModel Address { get; private set; }

        public bool IsValid
        {
            get
            {
                var validator = new CustomerValidator();
                var result = validator.Validate(this);
                return result.Errors.Count == 0;
            }
        }

        public static CustomerModel CreateObject()
        {
            var customer = new CustomerModel();
            return customer;
        }

        public static CustomerModel CreateObject(CustomerType customerType, string firstName, string lastName, DateTime dateOfBirth)
        {
            var customer = CreateObject();
            customer.CustomerType = customerType;
            customer.FirstName = firstName;
            customer.LastName = lastName;
            customer.DateOfBirth = dateOfBirth;
            customer.CustomerNumber = Guid.NewGuid().ToString();
            if (!customer.IsValid)
                throw new InvalidOperationException();
            return customer;
        }

        public static void SetAddress(CustomerModel model, string street, string city, string postcode)
        {
            model.Address = AddressModel.CreateObject(street, city, postcode);
        }
    }
}
