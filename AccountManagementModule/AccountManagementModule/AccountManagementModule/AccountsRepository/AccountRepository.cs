using AccountManagementModule.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AccountManagementModule.AccountsRepository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDBContext newContext;
        private readonly IHttpContextAccessor newHttpContextAccessor;

        public AccountRepository(AccountDBContext context, IHttpContextAccessor httpContextAccessor)
        {
            newContext = context;
            newHttpContextAccessor = httpContextAccessor;
        }

        public bool CreateAccount(int customerId)
        {
            try
            {
                newContext.Accounts.Add(new Account() { AccountType = AccountType.Saving, Balance = 0, CustomerId = customerId });
                newContext.Accounts.Add(new Account() { AccountType = AccountType.Current, Balance = 0, CustomerId = customerId });
                newContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Account> GetCustomerAccounts(int customerId)
        {
            try
            {
                List<Account> accounts = newContext.Accounts.Where(c => c.CustomerId == customerId).ToList();
                return accounts;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Account GetAccount(int accountId)
        {
            try
            {
                Account account = newContext.Accounts.Where(c => c.AccountId == accountId).SingleOrDefault();
                return account;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public bool Deposit(InputAmountFromUser amountClass)
        {
            try
            {
                Account acc = newContext.Accounts.FirstOrDefault(a => a.AccountId == amountClass.AccountId);
                acc.Balance += amountClass.Amount;
                newContext.TransactionStatuses.Add(new TransactionStatus() { AccountId = amountClass.AccountId, Message = "Success", currentBalance = acc.Balance });
                newContext.Statements.Add(new Statement() { AccountId = amountClass.AccountId, Date = DateTime.Now, Narration = amountClass.Narration, Deposit = amountClass.Amount, Withdrawal = 0, ClosingBalance = acc.Balance, RefNo = $"Ref_No_{amountClass.AccountId}_{DateTime.Now.ToShortDateString()}", ValueDate = DateTime.Now });
                newContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Withdraw(InputAmountFromUser amountClass)
        {
            try
            {
                // int customerId = int.Parse(newHttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
               // int customerId = 3;
                
                Account acc = newContext.Accounts.FirstOrDefault(a => a.AccountId == amountClass.AccountId /*&& customerId == a.CustomerId*/);
                
                if (acc == null)
                    return false;
                if ((acc.Balance - amountClass.Amount) < 0)
                    return false;
                acc.Balance -= amountClass.Amount;
                newContext.TransactionStatuses.Add(new TransactionStatus() { AccountId = amountClass.AccountId, Message = "Success", currentBalance = acc.Balance });
                newContext.Statements.Add(new Statement() { AccountId = amountClass.AccountId, Date = DateTime.Now, Narration = amountClass.Narration, Deposit = 0, Withdrawal = amountClass.Amount, ClosingBalance = acc.Balance, RefNo = $"Ref_No_{amountClass.AccountId}_{DateTime.Now.ToShortDateString()}", ValueDate = DateTime.Now });
                newContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Statement> GetStatements(int accountId, string from_date, string to_date)
        {
            try
            {
                DateTime fromDate;
                DateTime toDate;
                if (from_date != null && to_date != null)
                {
                    fromDate = DateTime.ParseExact(from_date, "yyyy-MM-dd", null);
                    toDate = DateTime.ParseExact(to_date, "yyyy-MM-dd", null).AddDays(1);
                }
                else
                {
                    fromDate = DateTime.Now.AddMonths(-1);
                    toDate = DateTime.Now.AddDays(1);
                }
                List<Statement> statements = newContext.Statements.Where(c => c.Date >= fromDate && c.Date <= toDate && c.AccountId == accountId).ToList();
                return statements;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
