using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BankPortal.Services
{
    public interface IAccountService
    {
        Task<HttpResponseMessage> GetAccount(int accountId);
        Task<HttpResponseMessage> GetCustomerAccounts(int customerId);
        Task<HttpResponseMessage> GetAccountStatement(int accountId, string from_date, string to_date);
    }
}
