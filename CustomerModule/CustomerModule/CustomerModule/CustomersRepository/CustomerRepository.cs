using CustomerModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerModule.CustomersRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext newContext;
        private readonly IAccountService newAccountService;

        public CustomerRepository(CustomerDbContext context, IAccountService accountService)
        {
            newContext = context;
            newAccountService = accountService;
        }

        public List<Customer> GetAllCustomers()
        {
            try
            {
                List<Customer> customers = newContext.Customers.ToList();
                return customers;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public CustomerCreationStatus CreateCustomer(Customer customer)
        {
            try
            {
                Customer customerinDB = newContext.Customers.Where(c => c.PAN == customer.PAN).SingleOrDefault();
                if (customerinDB != null)
                    return new CustomerCreationStatus { Message = "Customer with Pan Number is already existed" };
                newContext.Customers.Add(customer);
                newContext.SaveChanges();
                bool success = newAccountService.CreateAccount(customer.CustomerId);
                if (success)
                {
                    return new CustomerCreationStatus { CustomerId = customer.CustomerId, Message = "CustomerAccount is Created Successfully" };
                }
                else
                    return new CustomerCreationStatus { Message = "Error while creating Account" };

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Customer GetCustomerDetails(int customerId)
        {
            try
            {
                Customer customer = newContext.Customers.Find(customerId);
                if (customer == null)
                    return null;
                return customer;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public CustomerResponse GetCustomer(CustomerRequest customerRequest)
        {
            try
            {
                Customer customer = newContext.Customers.Where(c => c.Email == customerRequest.Email && c.Password == customerRequest.Password).FirstOrDefault();
                if (customer == null)
                    return null;
                return new CustomerResponse { Id = customer.CustomerId };
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
