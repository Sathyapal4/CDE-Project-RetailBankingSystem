using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RulesAPI.Models;
using RulesAPI.RulesRepository;
using System;
using System.Collections.Generic;

namespace RulesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RulesController : ControllerBase
    {
        private readonly IRulesRepository newRulesRepository;

        public RulesController(IRulesRepository rulesRepository)
        {
            newRulesRepository = rulesRepository;
        }

        [HttpGet("[action]/{AccountId:int}")]
        public IActionResult evaluateMinBal (int AccountId)
        {
            try
            {
                if (AccountId == 0)
                    return BadRequest();
                RuleStatus ruleStatus = newRulesRepository.getMinimumBalance(AccountId);
                if (ruleStatus == null)
                    return BadRequest();
                return Ok(ruleStatus);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw e;
            }

        }
        [HttpGet("[action]")]
        public IActionResult getServiceCharges()
        {
            try
            {
                List<ServiceCharge> serviceChargeResponses = new List<ServiceCharge>();
                serviceChargeResponses = newRulesRepository.getServiceCharges();
                if (serviceChargeResponses == null)
                    return NoContent();
                return Ok(serviceChargeResponses);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw e;
            }
        }
    }
}
