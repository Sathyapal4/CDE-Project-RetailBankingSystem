using CustomerModule.Models;
using CustomerModule.CustomersRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CustomerModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
  //  [Authorize(Roles = "Employee")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository newCustomerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            newCustomerRepository = customerRepository;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllCustomers()
        {
            try
            {
                List<Customer> customers = newCustomerRepository.GetAllCustomers();
                if (customers == null)
                    return NotFound();
                return Ok(customers);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching customer");
                throw e;
            }
        }

        [HttpGet]
        [Route("getCustomerDetails/{customer_Id}")]
        public IActionResult GetCustomerDetails(int customer_Id)
        {
            try
            {
                Customer customer = newCustomerRepository.GetCustomerDetails(customer_Id);
                if (customer == null)
                    return NotFound("Customer with this Id is not exist");
                return Ok(customer);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching customer");
                throw e;
            
            }
        }

        [HttpPost]
        [Route("createCustomer")]
        public IActionResult CreateCustomer([FromBody] Customer customer)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest("Improper Customer Data");
                CustomerCreationStatus customerCreationStatus = newCustomerRepository.CreateCustomer(customer);
                if (customerCreationStatus.CustomerId != null)
                    return StatusCode(StatusCodes.Status201Created, customerCreationStatus);
                else
                    return BadRequest(customerCreationStatus);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching customer");
                throw e;
            }
        }

        [HttpPost]
        [Route("checkCredentials")]
        [AllowAnonymous]
        public IActionResult CheckCustomerCredentials([FromBody] CustomerRequest customerRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid Email And Password");
                CustomerResponse result = newCustomerRepository.GetCustomer(customerRequest);
                if (result != null)
                    return Ok(result);
                return BadRequest("Invalid Email And Password");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Try again later");
                throw e;
            }
        }
    }
}
