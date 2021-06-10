using TransactionsModule.Models;

namespace TransactionsModule.Services
{
    public interface IAccountService
    {
        AmountResponse Deposit(Account account);
        bool GetAccountId(int accountId);
        AmountResponse WithDraw(Account account);
    }
}