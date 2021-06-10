using RulesAPI.Models;
using System.Collections.Generic;

namespace RulesAPI.RulesRepository
{
    public interface IAccountService
    {
        Account GetAccount(int accountId);
        List<Account> GetAllAccounts();
    }
}