using AccountManagementModule.Models;
using AccountManagementModule.AccountsRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;

namespace AccountAPI.Tests
{
    [TestFixture]
    public class TestAccountRepository
    {
        private AccountDBContext context;
        [SetUp]
        public void SetUpDatabase()
        {
            var options = new DbContextOptionsBuilder<AccountDBContext>()
                              .UseInMemoryDatabase(databaseName: "AccMicroService")
                              .Options;
            context = new AccountDBContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
        [Test]
        public void GetCustomerAccountsWhenCalledwithProperCustomerIdToReturnListOfAccounts()
        {


            context.Accounts.Add(new Account
            {
                AccountId = 1,
                AccountType = AccountType.Saving,
                CustomerId = 1,
                Balance = 40000,

            });

            context.Accounts.Add(new Account
            {
                AccountId = 2,
                AccountType = AccountType.Current,
                CustomerId = 1,
                Balance = 50000,

            });
            context.SaveChanges();

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            AccountRepository repository = new AccountRepository(context, mockHttpContextAccessor.Object);
            var account = repository.GetCustomerAccounts(1);
            Assert.IsNotNull(account.Count);
            Assert.AreEqual(2, account.Count);



        }
        [Test]
        public void GetAccountsWhenCalledwithAccountIdToReturnAccountObject()
        {

            context.Accounts.Add(new Account
            {
                AccountId = 1,
                AccountType = AccountType.Saving,
                CustomerId = 1,
                Balance = 40000,

            });

            context.Accounts.Add(new Account
            {
                AccountId = 2,
                AccountType = AccountType.Current,
                CustomerId = 1,
                Balance = 50000,

            });
            context.SaveChanges();

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            AccountRepository repository = new AccountRepository(context, mockHttpContextAccessor.Object);
            var account = repository.GetAccount(1);
            Assert.IsNotNull(account);
            Assert.AreEqual(40000, account.Balance);


        }
        [Test]
        public void GetStatementsWhenCalledwithProperParametersTOReturnStatementList()
        {

            context.Statements.Add(new Statement
            {
                Id = 1,
                AccountId = 6,
                Narration = "Statement generated",
                RefNo = "1",
                Deposit = 40000,
                Date = new DateTime(2011, 3, 25),
                Withdrawal = 15000,
                Account = new Account
                {
                    AccountId = 6,
                    AccountType = AccountType.Saving,
                    CustomerId = 1,
                    Balance = 40000

                },
                ClosingBalance = 15000.00


            });
            context.Statements.Add(new Statement
            {
                Id = 2,
                AccountId = 4,
                Narration = "Statement generated",
                RefNo = "1",
                Deposit = 40000,
                Date = new DateTime(2011, 3, 12),
                Withdrawal = 15000,
                Account = new Account
                {
                    AccountId = 4,
                    AccountType = AccountType.Saving,
                    CustomerId = 1,
                    Balance = 40000

                },
                ClosingBalance = 15000.00


            });
            context.SaveChanges();
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            AccountRepository repository = new AccountRepository(context, mockHttpContextAccessor.Object);
            var account = repository.GetStatements(4, "2011-03-11", "2011-07-11");
            Assert.IsNotNull(account.Count);
            Assert.AreEqual(1, account.Count);

        }
        [Test]
        public void DepositWhenCalledWithProperCredentialAndReturnTrue()
        {

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            context.Accounts.Add(new Account { AccountId = 1, Balance = 0 });
            context.SaveChanges();

            AccountRepository repository = new AccountRepository(context, mockHttpContextAccessor.Object);
            InputAmountFromUser amountRequest = new InputAmountFromUser
            {
                AccountId = 1,
                Amount = 50000,
                Narration = "Amount Deposited"
            };
            var account = repository.Deposit(amountRequest);
            Assert.IsNotNull(account);
            Assert.IsTrue(account);

        }

    }

}
