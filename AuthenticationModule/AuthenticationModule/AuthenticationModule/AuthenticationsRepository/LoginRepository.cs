using AuthenticationModule.Models;
using log4net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationModule.AuthenticationsRepository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(LoginRepository));
        private readonly ICustomerService _customerService;
        private readonly IEmployeeService _employeeService;
        private readonly IConfiguration _configuration;

        public LoginRepository(ICustomerService customerService, IEmployeeService employeeService, IConfiguration configuration)
        {
            _customerService = customerService;
            _employeeService = employeeService;
            _configuration = configuration;
        }

        public UserResponse Login(UserRequest userRequest)
        {
            try
            {
                _logger.Info("Login Method called in Login Repository");
                if (userRequest.Role == Role.Customer)
                {
                    UserResponse userResponse = _customerService.CheckUser(userRequest);
                    if (userResponse != null)
                    {
                        string token = GenerateJsonWebToken(userResponse.Id, Role.Customer);
                        userResponse.Token = token;
                        userResponse.Message = "Login Successfull";
                        return userResponse;
                    }

                    else
                        return new UserResponse { Message = "Login Failed" };
                }
                else if (userRequest.Role == Role.Employee)
                {
                    UserResponse userResponse = _employeeService.CheckUser(userRequest);
                    if (userResponse != null)
                    {
                        string token = GenerateJsonWebToken(userResponse.Id, Role.Employee);
                        userResponse.Token = token;
                        userResponse.Message = "Login Successfull";
                        return userResponse;
                    }
                    else
                        return new UserResponse { Message = "Login Failed" };
                }
                return new UserResponse { Message = "Login Failed" };
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                throw;
            }
        }


        private string GenerateJsonWebToken(int customerId, Role role)
        {
            _logger.Info("Generate JsonWebToken");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,customerId.ToString()),
                new Claim(ClaimTypes.Role,role.ToString())
            };
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddMinutes(15),
                claims: claims,
                signingCredentials: signingCredentials
                );
            string token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            return token;
        }
    }
}
