using TransactionsModule.Models;

namespace TransactionsModule.Services
{
    public interface IRulesService
    {
        RuleStatus CheckMinimumBalance(Account account);
    }
}