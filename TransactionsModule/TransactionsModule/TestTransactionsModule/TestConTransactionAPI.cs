using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TransactionsModule.Controllers;
using TransactionsModule.Models;
using TransactionsModule.TransactionsRepository;

namespace TransactionsModule.Tests
{
    [TestFixture]
    public class TestConTransactionsModule
    {
        [Test]
        [TestCase(1)]
        public void GetTransaction(int id)
        {
            var mock = new Mock<ITransactionRepository>();
            mock.Setup(x => x.GetTransactions(id)).Returns(new List<Financial_Transaction>
            {
                new Financial_Transaction{ Account_ID=1, Transaction_ID=1},
                new Financial_Transaction{ Account_ID=2, Transaction_ID=2}

            });
            var controller = new TransactionController(mock.Object);
            var result = controller.GetTransactions(id) as ObjectResult;
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsNotNull(result.Value);
            var model = result.Value as List<Financial_Transaction>;
            Assert.AreEqual(2, model.Count);

        }
        [Test]
        public void PostMethodDeposit()
        {
            var mock = new Mock<ITransactionRepository>();
            Account account = new Account { AccountId = 1, Amount = 5000, Narration = "Hello" };
            mock.Setup(c => c.Deposit(account)).Returns(new Ref_Transaction_Status { Trans_Status_Code = 1, Trans_Status_Description = Trans_Status_Description.Completed });
            var controller = new TransactionController(mock.Object);
            var result = controller.Deposit(account) as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value);
            var model = result.Value as Ref_Transaction_Status;
            Assert.AreEqual(1, model.Trans_Status_Code);


        }
        [Test]
        public void PostMethodWithdraw()
        {
            var mock = new Mock<ITransactionRepository>();
            Account account = new Account { AccountId = 1, Amount = 8000, Narration = "Hello" };
            mock.Setup(c => c.Withdraw(account)).Returns(new Ref_Transaction_Status { Trans_Status_Code = 1, Trans_Status_Description = Trans_Status_Description.Completed });
            var controller = new TransactionController(mock.Object);
            var result = controller.WithDraw(account) as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value);
            var model = result.Value as Ref_Transaction_Status;
            Assert.AreEqual(1, model.Trans_Status_Code);


        }

        [Test]
        public void PostMethodTransfer()
        {
            var mock = new Mock<ITransactionRepository>();
            Transfer transfer = new Transfer { SourceAccountId = 1, Amount = 12000, DestinationAccountId = 3 };
            mock.Setup(c => c.Transfer(transfer)).Returns(new Ref_Transaction_Status { Trans_Status_Code = 1, Trans_Status_Description = Trans_Status_Description.Completed });
            var controller = new TransactionController(mock.Object);
            var result = controller.Transfer(transfer) as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value);
            var model = result.Value as Ref_Transaction_Status;
            Assert.AreEqual(1, model.Trans_Status_Code);


        }
    }
}




