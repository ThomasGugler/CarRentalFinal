namespace CarRental.Contracts
{
    using CarRental.Models;

    public interface ICustomerService
    {
        void AddCustomer(CustomerModel customerModel);

        CustomerModel GetCustomerByCustomerNumber(string customerNumber);
    }
}
