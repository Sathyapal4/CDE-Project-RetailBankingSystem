using CustomerModule.Controllers;
using CustomerModule.Models;
using CustomerModule.CustomersRepository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CustomerModule.Tests
{

    [TestFixture]
    class TestCustomersController
    {

        Mock<ICustomerRepository> mock = new Mock<ICustomerRepository>();
        CustomersController systemUnderTest;
        [OneTimeSetUp]
        public void SetUp()
        {
            mock = new Mock<ICustomerRepository>();

        }


        private static IEnumerable<List<Customer>> GetAllCustomers_TestCase
        {
            get
            {
                yield return new List<Customer> {
                new Customer{ Name="Abcdef",Address="abcdef",DOB=Convert.ToDateTime("1997-11-10"),PAN="ABCDEF1234",Email="abcdef@gmail.com",Password
                ="abcdef",ConfirmPassword="abcdef"} ,
                new Customer{ Name="Ghijkl",Address="ghijkl",DOB=Convert.ToDateTime("1982-05-05"),PAN="GHIJKL1234",Email="ghijkl@gmail.com",Password
                ="ghijkl",ConfirmPassword="ghijkl"}
            };
                // yield return new List<Customer> { };
            }
        }



        [Test]
        [TestCaseSource(nameof(GetAllCustomers_TestCase))]
        public void GetAllCustomers_Test(List<Customer> l)
        {
            mock.Setup(x => x.GetAllCustomers()).Returns(l);
            systemUnderTest = new CustomersController(mock.Object);
            var result = systemUnderTest.GetAllCustomers() as ObjectResult;
            Console.WriteLine(result.StatusCode);
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsNotNull(result.Value);
            var model = result.Value as List<Customer>;
            Assert.AreEqual(2, model.Count);

        }


        [Test]
        [TestCase("Nopqrs", "nopqrs", "1972-11-10", "NOPQRS1234", "nopqrs@gmail.com", "nopqrs456", "nopqrs456")]
        public void CreateCustomer_ValidInput(string name, string address, DateTime dob, string pan, string email, string password, string confirmpassword)
        {
            Customer customer = new Customer
            {
                Name = name,
                Address = address,
                DOB = dob,
                PAN = pan,
                Email = email,
                Password = password,
                ConfirmPassword = confirmpassword

            };
            mock.Setup(x => x.CreateCustomer(customer)).Returns(new CustomerCreationStatus { CustomerId = customer.CustomerId, Message = "CustomerAccount is Created Successfully" });
            systemUnderTest = new CustomersController(mock.Object);

            var result = systemUnderTest.CreateCustomer(customer) as ObjectResult;
            Assert.AreEqual(result.StatusCode, 201);
            Assert.IsNotNull(result.Value);
        }


        [Test]
        [TestCase("Abcdef", "abcdef", "1997-11-10", "ABCDEF1234", "abcdef@gmail.com", "abcdef456", "abcdef456")]
        public void CreateCustomer_CustomerExists_BadRequest(string name, string address, DateTime dob, string pan, string email, string password, string confirmpassword)
        {
            Customer customer = new Customer
            {
                Name = name,
                Address = address,
                DOB = dob,
                PAN = pan,
                Email = email,
                Password = password,
                ConfirmPassword = confirmpassword

            };
            mock.Setup(x => x.CreateCustomer(customer)).Returns(new CustomerCreationStatus { Message = "Customer with Pan Number is already existed" });
            systemUnderTest = new CustomersController(mock.Object);

            var result = systemUnderTest.CreateCustomer(customer) as ObjectResult;
            Assert.AreEqual(result.StatusCode, 400);

        }

        [Test]
        //[TestCase(1,200)]
        [TestCase(4, 404)]
        public void GetCustomerDetailsTest(int id, int expected)
        {
            mock.Setup(x => x.GetAllCustomers()).Returns(new List<Customer> {
                new Customer {CustomerId=1, Name = "Dhanshri", Address = "rtyuioasdfghjkl", DOB = Convert.ToDateTime("1998-02-01"), PAN = "BRAKGK6468", Email = "nandedhanshri@gmail.com", Password = "qwertnio", ConfirmPassword = "qwertnio" },
                new Customer {CustomerId=2, Name = "Divya", Address = "zxcvbnqwertyu", DOB = Convert.ToDateTime("1999-02-01"), PAN = "JKLMNO2345", Email = "divya@gmail.com", Password = "asdfghop", ConfirmPassword = "asdfgop" }
            });
            systemUnderTest = new CustomersController(mock.Object);
            var result = systemUnderTest.GetCustomerDetails(id) as ObjectResult;
            Console.WriteLine(result.Value);
            Assert.AreEqual(expected, result.StatusCode);
            if (id > 2) Assert.IsNotNull(result.Value);
        }
        [Test]
        public void CheckCustomerCredentialsTest()
        {
            var mock = new Mock<ICustomerRepository>();
            CustomerRequest customerRequest = new CustomerRequest { Email = "vrajshah363@gmail.com", Password = "Vraj12345" };
            Customer customer = new Customer { CustomerId = 1, Name = "Dhanshri", Address = "rtyuio", DOB = Convert.ToDateTime("1998-12-21"), PAN = "BRAKGK6468", Email = "nandedhanshri@gmail.com", Password = "qwertn", ConfirmPassword = "qwertn" };
            mock.Setup(c => c.GetCustomer(customerRequest)).Returns(new CustomerResponse { Id = 1 });
            var controller = new CustomersController(mock.Object);
            var result = controller.CheckCustomerCredentials(customerRequest) as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value);
            var model = result.Value as CustomerResponse;
            Assert.AreEqual(1, model.Id);
        }

    }
}
