using AuthenticationModule.Models;
using AuthenticationModule.AuthenticationsRepository;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AuthenticationModule.AuthenticationsRepository
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoginRepository newLoginRepository;

        public AuthenticationController(ILoginRepository loginRepository)
        {
            newLoginRepository = loginRepository;
        }
        [HttpPost("[action]")]
        public IActionResult Login([FromBody] UserRequest userRequest)
        {
            try
            {
                UserResponse response = newLoginRepository.Login(userRequest);
                if (response.Id != 0)
                    return Ok(response);
                return BadRequest(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Try Again After Some Time");
                throw e;
            }
        }
    }
}
