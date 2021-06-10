using System.Collections.Generic;
using TransactionsModule.Models;

namespace TransactionsModule.TransactionsRepository
{
    public interface ITransactionRepository
    {
        Ref_Transaction_Status Deposit(Account account);
        List<Financial_Transaction> GetTransactions(int accountId);
        Ref_Transaction_Status Transfer(Transfer transfer);
        Ref_Transaction_Status Withdraw(Account account);
    }
}