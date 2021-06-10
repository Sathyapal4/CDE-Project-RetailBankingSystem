using AccountManagementModule.Models;
using AccountManagementModule.AccountsRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AccountManagementModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository newAccountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            newAccountRepository = accountRepository;
        }

        [HttpPost("[action]")]
       // [Authorize(Roles = "Employee")]
        public IActionResult CreateAccount([FromBody] CustomerID customerID)
        {
            try
            {
                bool success = newAccountRepository.CreateAccount(customerID.CustomerId);
                if (success)
                    return StatusCode(StatusCodes.Status201Created);
                else
                    return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw e;
            }

        }
        [HttpGet("[action]/{customerId}")]
        public IActionResult GetCustomerAccounts(int customerId)
        {
            try
            {
                List<Account> accounts = newAccountRepository.GetCustomerAccounts(customerId);
                if (accounts == null)
                    return NotFound("Zero accounts found for the given CustomerID");
                else
                    return Ok(accounts);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw e;
            }


        }


        [HttpGet("[action]/{accountId}")]
        public IActionResult GetAccount(int accountId)
        {
            try
            {
                Account account = newAccountRepository.GetAccount(accountId);
                if (account == null)
                    return NotFound("Zero accounts found for the given AccountID");
                else
                    return Ok(account);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw e;
            }

        }


        [HttpPost("[action]")]
        public IActionResult Deposit([FromBody] InputAmountFromUser amountClass)
        {
            try
            {
                bool success = newAccountRepository.Deposit(amountClass);
                if (success)
                    return Ok();
                return BadRequest();
            }

            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw e;
            }
        }


        [HttpPost("[action]")]
        public IActionResult Withdraw([FromBody] InputAmountFromUser amountClass)
        {
            try
            {
                bool success = newAccountRepository.Withdraw(amountClass);
                if (success)
                    return Ok();
                return BadRequest(new { Message = "Withdrawn amount is higher than balance or This account doesn't exist." });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw e;
            }

        }

        [HttpGet("[action]/{accountId}/{from_date?}/{to_date?}")]
        public IActionResult GetStatement(int accountId, string from_date = null, string to_date = null)
        {
            try
            {
                List<Statement> statements = newAccountRepository.GetStatements(accountId, from_date, to_date);
                if (statements == null)
                    return NotFound("No Statements");
                else
                    return Ok(statements);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw e;
            }
        }
    }
}
