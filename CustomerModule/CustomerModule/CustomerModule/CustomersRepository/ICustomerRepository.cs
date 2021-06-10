using CustomerModule.Models;
using System.Collections.Generic;

namespace CustomerModule.CustomersRepository
{
    public interface ICustomerRepository
    {
        CustomerCreationStatus CreateCustomer(Customer customer);
        Customer GetCustomerDetails(int customerId);
        CustomerResponse GetCustomer(CustomerRequest customerRequest);
        List<Customer> GetAllCustomers();
    }
}
