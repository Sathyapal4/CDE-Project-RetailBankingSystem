using CustomerModule.Models;
using CustomerModule.CustomersRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerModule.Tests
{
    [TestFixture]
    class TestCustomersRepository
    {
        private DbContextOptions<CustomerDbContext> options;
        Mock<IConfiguration> mock = new Mock<IConfiguration>();
        Mock<IHttpContextAccessor> mock1 = new Mock<IHttpContextAccessor>();
        AccountService accountService;
        [OneTimeSetUp]
        public void Setup()
        {
            accountService = new AccountService(mock.Object,mock1.Object);
            //Create In Memory Database
             options = new DbContextOptionsBuilder<CustomerDbContext>()
            .UseInMemoryDatabase(databaseName: "CustomerDataBase").Options;

            //// Create mocked Context by seeding Data as per Schema///

            using (var context = new CustomerDbContext(options))
            {
                context.Customers.Add(new Customer
                {
                    Name = "Abcdef",
                    Address = "abcdef",
                    DOB = Convert.ToDateTime("1997-11-10"),
                    PAN = "ABCDEF1234",
                    Email = "abcdef@gmail.com",
                    Password = "abcdef",
                    ConfirmPassword = "abcdef"
                });

                context.Customers.Add(new Customer
                {
                    Name = "Ghijkl",
                    Address = "ghijkl",
                    DOB = Convert.ToDateTime("1982-05-05"),
                    PAN = "GHIJKL1234",
                    Email = "ghijkl@gmail.com",
                    Password = "ghijkl",
                    ConfirmPassword = "ghijkl"
                });
                context.SaveChanges();
            }
        }
        [Test]
        public void CustomerRepository_GetAllCustomers_Test()
        {
           
            using (var context = new CustomerDbContext(options))
            {
                CustomerRepository customerRepository = new CustomerRepository(context, accountService);
                var customers = customerRepository.GetAllCustomers();
                Assert.AreEqual(2, customers.Count);
            }
        }

        [Test]
        [TestCase(1)]
        [TestCase(4)]
        public void CustomerRepository_GetCustomerDetails_Test(int id)
        {

            using (var context = new CustomerDbContext(options))
            {
                CustomerRepository customerRepository = new CustomerRepository(context, accountService);  
                
                if(id>3)
                {
                    // null values for id not in db
                    Assert.IsNull(customerRepository.GetCustomerDetails(id));
                }
                else
                {
                    // id found in db
                    var customer = customerRepository.GetCustomerDetails(id);
                    Assert.AreEqual(customer.Name, context.Customers.Find(id).Name);
                }

            }
        }

        [Test]
        [TestCase(1,"abcdef@gmail.com","abcdef")]
        [TestCase(2, "ghijkl@gmail.com", "ghijkl")]
        public void GetCustomer_Test(int expectedId,string email, string password)
        {
            using (var context = new CustomerDbContext(options))
            {
                CustomerRequest customerRequest = new CustomerRequest { Email = email, Password = password };
                CustomerRepository customerRepository = new CustomerRepository(context, accountService);
                var customers = customerRepository.GetCustomer(customerRequest);
                Console.WriteLine(customers.Id);
                Assert.AreEqual(expectedId, customers.Id);
            }
        }


        //[Test]
        //[TestCase("Nopqrs", "nopqrs", "1972-11-10", "NOPQRS1234", "nopqrs@gmail.com", "nopqrs456", "nopqrs456")]
        //public void CreateCustomerTests(string name, string address, DateTime dob, string pan, string email, string password, string confirmpassword)
        //{

        //    Customer customer = new Customer
        //    {
        //        Name = name,
        //        Address = address,
        //        DOB = dob,
        //        PAN_Number = pan,
        //        Email = email,
        //        Password = password,
        //        ConfirmPassword = confirmpassword

        //    };
        //    using (var context = new CustomerDbContext(options))
        //    {
        //        CustomerRepository customerRepository = new CustomerRepository(context, accountService);
        //        var customers = customerRepository.CreateCustomer(customer);
        //        Assert.AreEqual("CustomerAccount is Created Successfully", customers.Message);
        //    }
        //}


    }
}
