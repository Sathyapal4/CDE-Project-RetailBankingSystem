using AuthenticationModule.Controllers;
using AuthenticationModule.Models;
using AuthenticationModule.AuthenticationsRepository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace AuthenticationAPI.Tests
{
    public class TestAuthenticationController
    {
        private Mock<ILoginRepository> newMockLogin;

        [SetUp]
        public void Intialize()
        {
            newMockLogin = new Mock<ILoginRepository>();
        }

        [Test]
        public void UserResponseWhenValidUserGetsLoggedIn()
        {
            newMockLogin.Setup(c => c.Login(It.IsAny<UserRequest>())).Returns(new UserResponse { Id = 1, Token = "Token Generated", Message = "Login Successfull" });
            var controller = new AuthenticationController(newMockLogin.Object);
            var result = controller.Login(new UserRequest { Email = "Sathya@gmail.com", Password = "Sathya@123" }) as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);

        }

        [Test]
        public void UserResponseWhenInValidUserGetsLoggedIn()
        {
            newMockLogin.Setup(c => c.Login(It.IsAny<UserRequest>())).Returns(new UserResponse { Message = "Login Successfull" });
            var controller = new AuthenticationController(newMockLogin.Object);
            var result = controller.Login(new UserRequest { Email = "Sathya4@gmail.com", Password = "Sathya@1234" }) as ObjectResult;
            Assert.AreEqual(400, result.StatusCode);

        }
    }
}
