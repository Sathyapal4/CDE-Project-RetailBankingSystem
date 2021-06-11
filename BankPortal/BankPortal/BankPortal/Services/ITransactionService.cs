using RetailBankingClient.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BankPortal.Services
{
    public interface ITransactionService
    {
        Task<HttpResponseMessage> Withdraw(Account withDraw);
        Task<HttpResponseMessage> Deposit(Account deposit);
        Task<HttpResponseMessage> Transfer(Transfer transfer);
        Task<HttpResponseMessage> GetTransactions(int id);
    }
}
