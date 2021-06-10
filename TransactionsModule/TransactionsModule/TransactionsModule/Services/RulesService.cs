using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using TransactionsModule.Models;

namespace TransactionsModule.Services
{
    public class RulesService : IRulesService
    {
        private readonly IConfiguration newConfiguration;
        private readonly IHttpContextAccessor newHttpContextAccessor;

        public RulesService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            newConfiguration = configuration;
            newHttpContextAccessor = httpContextAccessor;
        }

        public RuleStatus CheckMinimumBalance(Account account)
        {
            try
            {
                using (HttpClient _client = new HttpClient())
                {
                    StringValues token;
                    newHttpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out token);
                    _client.BaseAddress = new Uri(newConfiguration["BaseUrl:Rules"]);
                    _client.DefaultRequestHeaders.Add("Authorization", token.ToString());
                    HttpResponseMessage responseMessage = _client.GetAsync($"api/rules/EvaluateMinBalance/{account.AccountId}").Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        RuleStatus response = JsonConvert.DeserializeObject<RuleStatus>(responseMessage.Content.ReadAsStringAsync().Result);
                        return response;
                    }
                    return new RuleStatus { Status = Status.Denied };
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
