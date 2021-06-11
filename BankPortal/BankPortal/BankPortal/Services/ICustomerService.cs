using BankPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BankPortal.Services
{
    public interface ICustomerService
    {
        Task<HttpResponseMessage> CreateCustomer(Customer model);
        Task<HttpResponseMessage> GetCustomerDetails(int id);
        Task<HttpResponseMessage> GetCustomers();
    }
}
