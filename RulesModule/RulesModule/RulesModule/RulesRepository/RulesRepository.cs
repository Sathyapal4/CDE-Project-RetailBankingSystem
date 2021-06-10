using log4net;
using RulesAPI.Models;
using RulesAPI.Utilities;
using System;
using System.Collections.Generic;

namespace RulesAPI.RulesRepository
{
    public class RulesRepository : IRulesRepository
    {
        private readonly IAccountService _accountService;

        public RulesRepository(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public RuleStatus getMinimumBalance(int accountId)
        {
            try
            {
                Account account = _accountService.GetAccount(accountId);
                if (account == null)
                    return new RuleStatus { Status = Status.Denied };
                if (account.Balance < Charges.MinimumBalance)
                    return new RuleStatus { Status = Status.Denied };
                return new RuleStatus { Status = Status.Allowed };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ServiceCharge> getServiceCharges()
        {
            try
            {
                List<ServiceCharge> result = new List<ServiceCharge>();
                List<Account> accDetails = _accountService.GetAllAccounts();
                if (accDetails == null)
                    return null;
                foreach (Account item in accDetails)
                {
                    if (item.Balance < Charges.MinimumBalance)
                        result.Add(new ServiceCharge { AccountId = item.AccountId, WithdrawAmount = Charges.ServiceCharge });
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
