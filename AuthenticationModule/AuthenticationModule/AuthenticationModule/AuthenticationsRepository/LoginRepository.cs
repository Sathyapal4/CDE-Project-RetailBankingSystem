using AuthenticationModule.Models;
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
        private readonly ICustomerService newCustomerService;
        private readonly IEmployeeService newEmployeeService;
        private readonly IConfiguration newConfiguration;

        public LoginRepository(ICustomerService customerService, IEmployeeService employeeService, IConfiguration configuration)
        {
            newCustomerService = customerService;
            newEmployeeService = employeeService;
            newConfiguration = configuration;
        }

        public UserResponse Login(UserRequest userRequest)
        {
            try
            {
                if (userRequest.Role == Role.Customer)
                {
                    UserResponse userResponse = newCustomerService.CheckUser(userRequest);
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
                    UserResponse userResponse = newEmployeeService.CheckUser(userRequest);
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
                throw e;
            }
        }


        private string GenerateJsonWebToken(int customerId, Role role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(newConfiguration["JWT:SecretKey"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,customerId.ToString()),
                new Claim(ClaimTypes.Role,role.ToString())
            };
            var tokenDescriptor = new JwtSecurityToken(
                issuer: newConfiguration["JWT:Issuer"],
                audience: newConfiguration["JWT:Audience"],
                expires: DateTime.Now.AddMinutes(15),
                claims: claims,
                signingCredentials: signingCredentials
                );
            string token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            return token;
        }
    }
}
