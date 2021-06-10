using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using RulesAPI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace RulesAPI.RulesRepository
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration newConfiguration;
        private readonly IHttpContextAccessor newHttpContextAccessor;

        public AccountService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            newConfiguration = configuration;
            newHttpContextAccessor = httpContextAccessor;
        }

        //Calling GetAccount in Account
        public Account GetAccount(int accountId)
        {
            try
            {
                Account accDetailsList;
                using (HttpClient _client = new HttpClient())
                {
                    StringValues token;
                    newHttpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out token);
                    _client.BaseAddress = new Uri(newConfiguration["BaseUrl:Account"]);
                    _client.DefaultRequestHeaders.Add("Authorization", token.ToString());
                    HttpResponseMessage responseMessage = _client.GetAsync($"api/account/getAccount/{accountId}").Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        accDetailsList = JsonConvert.DeserializeObject<Account>(responseMessage.Content.ReadAsStringAsync().Result);
                        return accDetailsList;
                    }
                    return null;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Get all accounts in Account Service
        public List<Account> GetAllAccounts()
        {
            try
            {
                List<Account> accDetailsList = new List<Account>();
                using (HttpClient _client = new HttpClient())
                {
                    _client.BaseAddress = new Uri(newConfiguration["BaseUrl:Account"]);
                    HttpResponseMessage responseMessage = _client.GetAsync("api/account/getAllAccounts").Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        accDetailsList = JsonConvert.DeserializeObject<List<Account>>(responseMessage.Content.ReadAsStringAsync().Result);
                        return accDetailsList;
                    }
                    return null;
                }

            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
