using AccountManagementModule.Models;
using System.Collections.Generic;

namespace AccountManagementModule.AccountsRepository
{
    public interface IAccountRepository
    {
        bool CreateAccount(int customerId);
        bool Deposit(InputAmountFromUser amountClass);
        Account GetAccount(int accountId);
        List<Account> GetCustomerAccounts(int customerId);
        List<Statement> GetStatements(int accountId, string from_date, string to_date);
        bool Withdraw(InputAmountFromUser amountClass);
    }
}