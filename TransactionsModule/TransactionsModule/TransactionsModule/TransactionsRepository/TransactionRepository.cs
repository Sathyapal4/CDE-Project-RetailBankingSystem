using log4net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TransactionsModule.Models;
using TransactionsModule.Services;

namespace TransactionsModule.TransactionsRepository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionDbContext newContext;
        private readonly IAccountService newAccountService;

        public TransactionRepository(TransactionDbContext context, IAccountService accountService)
        {
            newContext = context;
            newAccountService = accountService;
        }


        //Call the deposit action of Account Microservice and pass account object
        public Ref_Transaction_Status Deposit(Account account)
        {
            try
            {
                


                AmountResponse response = newAccountService.Deposit(account);
                Ref_Transaction_Status ref_Transaction_Status;
                if (response.Success)
                {
                    newContext.Financial_Transactions.Add(
                        new Financial_Transaction
                        {
                            Account_ID = account.AccountId,
                            Counterparty_ID = 1,
                            Payment_Method_Code = 1,
                            Service_ID = 1,
                            Trans_Status_Code = 1,
                            Trans_Type_Code = 2,
                            Date_of_Transaction = DateTime.Now,
                            Amount_of_Transaction = account.Amount
                        }
                        );
                    ref_Transaction_Status = new Ref_Transaction_Status()
                    {
                        Trans_Status_Code = 1,
                        Trans_Status_Description = Trans_Status_Description.Completed
                    };

                }
                else
                {
                    newContext.Financial_Transactions.Add(
                       new Financial_Transaction
                       {
                           Account_ID = account.AccountId,
                           Counterparty_ID = 1,
                           Payment_Method_Code = 1,
                           Service_ID = 1,
                           Trans_Status_Code = 2,
                           Trans_Type_Code = 2,
                           Date_of_Transaction = DateTime.Now,
                           Amount_of_Transaction = account.Amount
                       }
                       );
                    ref_Transaction_Status = new Ref_Transaction_Status()
                    {
                        Trans_Status_Code = 2,
                        Trans_Status_Description = Trans_Status_Description.Disputed
                    };

                }
                newContext.SaveChanges();
                return ref_Transaction_Status;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Ref_Transaction_Status Withdraw(Account account)
        {
            try
            {
                //call Withdraw action of Account Microservice and pass Acc object
                AmountResponse response = newAccountService.WithDraw(account);
                Ref_Transaction_Status ref_Transaction_Status = null;
                if (response.Success)
                {
                    newContext.Financial_Transactions.Add(
                        new Financial_Transaction
                        {
                            Account_ID = account.AccountId,
                            Counterparty_ID = 1,
                            Payment_Method_Code = 1,
                            Service_ID = 1,
                            Trans_Status_Code = 1,
                            Trans_Type_Code = 2,
                            Date_of_Transaction = DateTime.Now,
                            Amount_of_Transaction = account.Amount
                        }
                        );
                    ref_Transaction_Status = new Ref_Transaction_Status()
                    {
                        Trans_Status_Code = 1,
                        Trans_Status_Description = Trans_Status_Description.Completed
                    };

                }
                else
                {
                    newContext.Financial_Transactions.Add(
                       new Financial_Transaction
                       {
                           Account_ID = account.AccountId,
                           Counterparty_ID = 1,
                           Payment_Method_Code = 1,
                           Service_ID = 1,
                           Trans_Status_Code = 2,
                           Trans_Type_Code = 2,
                           Date_of_Transaction = DateTime.Now,
                           Amount_of_Transaction = account.Amount
                       }
                       );
                    ref_Transaction_Status = new Ref_Transaction_Status()
                    {
                        Trans_Status_Code = 2,
                        Trans_Status_Description = Trans_Status_Description.Disputed
                    };

                }
                newContext.SaveChanges();
                return ref_Transaction_Status;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Ref_Transaction_Status Transfer(Transfer transfer)
        {
            try
            {
                Ref_Transaction_Status ref_Transaction_Status = null;
                bool success = newAccountService.GetAccountId(transfer.DestinationAccountId);
                if (success == false)
                    return null;
                //Call Withdraw microservice for rules
                AmountResponse response = newAccountService.WithDraw(new Account { AccountId = transfer.SourceAccountId, Amount = transfer.Amount });
                if (response.Success)
                {
                    AmountResponse response1 = newAccountService.Deposit(new Account { AccountId = transfer.DestinationAccountId, Amount = transfer.Amount });
                    if (response1.Success)
                    {
                        newContext.Financial_Transactions.Add(
                        new Financial_Transaction
                        {
                            Account_ID = transfer.SourceAccountId,
                            Counterparty_ID = 1,
                            Payment_Method_Code = 1,
                            Service_ID = 1,
                            Trans_Status_Code = 1,
                            Trans_Type_Code = 2,
                            Date_of_Transaction = DateTime.Now,
                            Amount_of_Transaction = transfer.Amount
                        }
                        );
                        newContext.Financial_Transactions.Add(
                        new Financial_Transaction
                        {
                            Account_ID = transfer.DestinationAccountId,
                            Counterparty_ID = 1,
                            Payment_Method_Code = 1,
                            Service_ID = 1,
                            Trans_Status_Code = 1,
                            Trans_Type_Code = 2,
                            Date_of_Transaction = DateTime.Now,
                            Amount_of_Transaction = transfer.Amount
                        }
                        );
                        ref_Transaction_Status = new Ref_Transaction_Status()
                        {
                            Trans_Status_Code = 1,
                            Trans_Status_Description = Trans_Status_Description.Completed
                        };
                    }
                }
                else
                {
                    newContext.Financial_Transactions.Add(
                       new Financial_Transaction
                       {
                           Account_ID = transfer.SourceAccountId,
                           Counterparty_ID = 1,
                           Payment_Method_Code = 1,
                           Service_ID = 1,
                           Trans_Status_Code = 2,
                           Trans_Type_Code = 2,
                           Date_of_Transaction = DateTime.Now,
                           Amount_of_Transaction = transfer.Amount
                       }
                       );
                    newContext.Financial_Transactions.Add(
                       new Financial_Transaction
                       {
                           Account_ID = transfer.DestinationAccountId,
                           Counterparty_ID = 1,
                           Payment_Method_Code = 1,
                           Service_ID = 1,
                           Trans_Status_Code = 2,
                           Trans_Type_Code = 2,
                           Date_of_Transaction = DateTime.Now,
                           Amount_of_Transaction = transfer.Amount
                       }
                       );
                    ref_Transaction_Status = new Ref_Transaction_Status()
                    {
                        Trans_Status_Code = 2,
                        Trans_Status_Description = Trans_Status_Description.Disputed
                    };
                }
                newContext.SaveChanges();
                return ref_Transaction_Status;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<Financial_Transaction> GetTransactions(int accountId)
        {
            try
            {
                List<Financial_Transaction> financial_Transactions = newContext.Financial_Transactions.Where(c => c.Account_ID == accountId)
                                                                                                    .Include(c => c.Ref_Payment_Methods)
                                                                                                    .Include(c => c.Ref_Transaction_Status)
                                                                                                    .Include(c => c.Ref_Transaction_Types)
                                                                                                    .ToList();
                return financial_Transactions;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
