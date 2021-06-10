using AccountManagementModule.AccountsRepository;
using AccountManagementModule.Controllers;
using AccountManagementModule.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace AccountAPI.Tests
{
    [TestFixture]
    class TestAccountController
    {
        [Test]
        [TestCase(1)]
        public void GetCustomerAccountsWhenCalledWithProperIdReturnsListOfAccounts(int id)
        {
            var mock = new Mock<IAccountRepository>();
            mock.Setup(x => x.GetCustomerAccounts(id)).Returns(new List<Account> {
                new Account{AccountId=1,Balance=0,CustomerId=id,AccountType=AccountType.Current},
                new Account{AccountId=2,Balance=0,CustomerId=id,AccountType=AccountType.Saving}
            });
            var controller = new AccountController(mock.Object);
            var result = controller.GetCustomerAccounts(id) as ObjectResult;
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsNotNull(result.Value);
            var model = result.Value as List<Account>;//List<Account>;
            Assert.AreEqual(2, model.Count);
        }
        [Test]
        [TestCase(1)]

        public void GetCustomerAccountsWhenIdIsInValidReturnsMessage(int id)
        {
            var mock = new Mock<IAccountRepository>();
            mock.Setup(x => x.GetCustomerAccounts(id)).Returns((List<Account>)null);

            string s = "Zero accounts found for the given CustomerID";

            var controller = new AccountController(mock.Object);
            var result = controller.GetCustomerAccounts(id) as ObjectResult;
            Assert.AreEqual(s, result.Value);
            Assert.AreEqual(404, result.StatusCode);

        }
        [Test]
        [TestCase(1)]
        public void GetAccountWhenCalledWithProperIdReturnsObjectOfAccount(int id)
        {
            var mock = new Mock<IAccountRepository>();
            mock.Setup(x => x.GetAccount(id))
                      .Returns(new Account { AccountId = 1, Balance = 0, CustomerId = id, AccountType = AccountType.Current });
            var controller = new AccountController(mock.Object);
            var result = controller.GetAccount(id) as ObjectResult;
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsNotNull(result.Value);
            var model = result.Value as Account;
            Assert.AreEqual(1, model.AccountId);
        }
        [Test]
        [TestCase(1)]
        public void GetAccountWhenIdIsInValidReturnsMessage(int id)
        {
            var mock = new Mock<IAccountRepository>();
            mock.Setup(x => x.GetAccount(id)).Returns((Account)null);
            var controller = new AccountController(mock.Object);
            var result = controller.GetAccount(id) as ObjectResult;
            string s = "Zero accounts found for the given AccountID";
            Assert.AreEqual(result.StatusCode, 404);
            Assert.AreEqual(s, result.Value);
        }

        [Test]
        [TestCase(1, "14/01/1999", "15/01/1999")]
        public void GetStatementsWhenCalledAndReturnStatement(int accountId, string from_date, string to_date)
        {
            var mock = new Mock<IAccountRepository>();
            mock.Setup(x => x.GetStatements(accountId, from_date, to_date)).Returns(new List<Statement> {
                new Statement { Id=1,AccountId=1 },
                new Statement { Id=2,AccountId=1 }
            });
            var controller = new AccountController(mock.Object);
            var result = controller.GetStatement(accountId, from_date, to_date) as ObjectResult;
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsNotNull(result.Value);
            var model = result.Value as List<Statement>;
            Assert.AreEqual(2, model.Count);
        }

        [Test]
        [TestCase(1, "14/01/1999", "15/01/1999")]
        public void GetStatementsWhenCalledWithInvalidIdReturnNoStatementMessage(int accountId, string from_date, string to_date)
        {
            var mock = new Mock<IAccountRepository>();
            mock.Setup(x => x.GetStatements(accountId, from_date, to_date)).Returns((List<Statement>)null);
            var controller = new AccountController(mock.Object);
            var result = controller.GetStatement(accountId, from_date, to_date) as ObjectResult;
            string s = "No Statements";
            Assert.AreEqual(result.StatusCode, 404);
            Assert.AreEqual(s, result.Value);
        }



        [Test]

        public void DepositWhenCalledWithProperAmountAndReturnTrue()
        {

            var mock = new Mock<IAccountRepository>();
            InputAmountFromUser amount = new InputAmountFromUser { AccountId = 1, Amount = 1000, Narration = "DimaagaKharaabHaiMera" };
            mock.Setup(x => x.Deposit(amount)).Returns(true);
            var controller = new AccountController(mock.Object);
            var result = controller.Deposit(amount);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<OkResult>());

        }
        [Test]
        public void WithdrawWhenCalledWithProperAmountAndReturnTrue()
        {

            var mock = new Mock<IAccountRepository>();
            InputAmountFromUser amount = new InputAmountFromUser { AccountId = 1, Amount = 1000, Narration = "DimaagaKharaabHaiMera" };
            mock.Setup(x => x.Withdraw(amount)).Returns(true);
            var controller = new AccountController(mock.Object);
            var result = controller.Withdraw(amount);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<OkResult>());

        }


    }
}
