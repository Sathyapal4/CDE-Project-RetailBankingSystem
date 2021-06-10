using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using TransactionsModule.Models;
using TransactionsModule.TransactionsRepository;
using TransactionsModule.Services;
using Microsoft.Extensions.Configuration;



namespace TransactionsModule.Tests
{


    [TestFixture]
    class TransactionRespositoryTest
    {
        TransactionRepository transactionRepositoty;
        Mock<IAccountService> mockAccountService;



        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<TransactionDbContext>()
                           .UseInMemoryDatabase(databaseName: "TransactionDataBase")
                           .Options;
            var context = new TransactionDbContext(options);
            mockAccountService = new Mock<IAccountService>();
            transactionRepositoty = new TransactionRepository(context, mockAccountService.Object);


        }
        [Test]
        [TestCase(1, 200, "Transfer Fund")]
        public void TransactionRepository_DepositAmount_ReturnStatus(int id, double amount, string narration)
        {
            Account account = new Account()
            {
                AccountId = id,
                Amount = amount,
                Narration = narration
            };
            mockAccountService.Setup(c => c.Deposit(account)).Returns(new AmountResponse { Message = "Amount Deposited Successfull", Success = true });
            var result = transactionRepositoty.Deposit(account);
            Assert.AreEqual(result.Trans_Status_Code, 1);

        }
        [Test]
        [TestCase(-1, 200, "Transfer Fund")]
        public void TransactionRepository_DepositAmount_ReturnStatusDisputed(int id, double amount, string narration)
        {
            Account account = new Account()
            {
                AccountId = id,
                Amount = amount,
                Narration = narration
            };
            mockAccountService.Setup(c => c.Deposit(account)).Returns(new AmountResponse { Message = "Error while Deposit Amount", Success = false });
            var result = transactionRepositoty.Deposit(account);
            Assert.AreEqual(result.Trans_Status_Code, 2);

        }

        [Test]
        [TestCase(11, 200, "WithDrwan Fund")]
        public void TransactionRepository_WithDraw_ReturnStatusCompletes(int id, double amount, string narration)
        {
            Account account = new Account()
            {
                AccountId = id,
                Amount = amount,
                Narration = narration
            };

            mockAccountService.Setup(c => c.WithDraw(account)).Returns(new AmountResponse { Message = "Amount Withdraw Successfull", Success = true });
            var result = transactionRepositoty.Withdraw(account);
            Assert.AreEqual(result.Trans_Status_Code, 1);
        }

        [Test]
        [TestCase(-11, 200, "WithDrwan Fund")]
        public void TransactionRepository_WithDraw_ReturnStatusDisputed(int id, double amount, string narration)
        {
            Account account = new Account()
            {
                AccountId = id,
                Amount = amount,
                Narration = narration
            };

            mockAccountService.Setup(c => c.WithDraw(account)).Returns(new AmountResponse { Message = "Error while Withdraw Amount", Success = false });
            var result = transactionRepositoty.Withdraw(account);
            Assert.AreEqual(result.Trans_Status_Code, 2);
        }


        [Test]
        [TestCase(1, 2, 200)]
        public void TransactionRepository_Transfer_ReturnStatusCompleted(int sourceid, int targetid, double amount)
        {
            Transfer transfer = new Transfer()
            {
                SourceAccountId = sourceid,
                DestinationAccountId = targetid,
                Amount = amount
            };

            Account account = new Account()
            {
                AccountId = sourceid,
                Amount = 100,

            };
            Account account1 = new Account()
            {
                AccountId = targetid,
                Amount = 100,
            };
            AmountResponse amountResponse = new AmountResponse { Success = true };
            mockAccountService.Setup(c => c.GetAccountId(It.IsAny<int>())).Returns(true);
            mockAccountService.Setup(c => c.WithDraw(It.IsAny<Account>())).Returns(amountResponse);
            mockAccountService.Setup(c => c.Deposit(It.IsAny<Account>())).Returns(amountResponse);
            var result = transactionRepositoty.Transfer(transfer);
            Assert.AreEqual(1, result.Trans_Status_Code);
        }
    }
}
