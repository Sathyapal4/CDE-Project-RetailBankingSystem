using RulesAPI.Models;
using System.Collections.Generic;

namespace RulesAPI.RulesRepository
{
    public interface IRulesRepository
    {
        RuleStatus getMinimumBalance(int accountId);
        List<ServiceCharge> getServiceCharges();
    }
}