using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using TransactionsModule.Models;

namespace TransactionsModule.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration newConfiguration;
        private readonly IRulesService newRulesService;
        private readonly IHttpContextAccessor newHttpContextAccessor;

        public AccountService(IConfiguration configuration, IRulesService rulesService, IHttpContextAccessor httpContextAccessor)
        {
            newConfiguration = configuration;
            newRulesService = rulesService;
            newHttpContextAccessor = httpContextAccessor;
        }

        //Deposit method in Account Service
        public AmountResponse Deposit(Account account)
        {
            try
            {
                using (HttpClient _client = new HttpClient())
                {
                    StringValues token;
                    newHttpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out token);
                    _client.BaseAddress = new Uri(newConfiguration["BaseUrl:Account"]);
                    _client.DefaultRequestHeaders.Add("Authorization", token.ToString());
                    var stringPayload = JsonConvert.SerializeObject(account);
                    var payload = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                    HttpResponseMessage responseMessage = _client.PostAsync("api/account/deposit", payload).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return new AmountResponse { Message = "Amount Deposited Successfull", Success = true };
                    }
                    return new AmountResponse { Message = "Error while Deposit Amount", Success = false };
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Withdraw
        public AmountResponse WithDraw(Account account)
        {
            try
            {

                //RulesService

                RuleStatus ruleStatus = newRulesService.CheckMinimumBalance(account);
                if (ruleStatus.Status == Status.Denied)
                    return new AmountResponse { Message = "Balance is Less Than Minimum Balance", Success = false };
                using (HttpClient _client = new HttpClient())
                {
                    StringValues token;
                    newHttpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out token);
                    _client.BaseAddress = new Uri(newConfiguration["BaseUrl:Account"]);
                    _client.DefaultRequestHeaders.Add("Authorization", token.ToString());
                    var stringPayload = JsonConvert.SerializeObject(account);
                    var payload = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                    HttpResponseMessage responseMessage = _client.PostAsync("api/account/withdraw", payload).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return new AmountResponse { Message = "Amount Withdraw Successfull", Success = true };
                    }
                    return new AmountResponse { Message = "Error while Withdraw Amount", Success = false };
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }



        //getAccountId
        public bool GetAccountId(int accountId)
        {
            try
            {
                using (HttpClient _client = new HttpClient())
                {
                    StringValues token;
                    newHttpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out token);
                    _client.BaseAddress = new Uri(newConfiguration["BaseUrl:Account"]);
                    _client.DefaultRequestHeaders.Add("Authorization", token.ToString());
                    HttpResponseMessage responseMessage = _client.GetAsync("api/Account/GetAccount/" + accountId).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
