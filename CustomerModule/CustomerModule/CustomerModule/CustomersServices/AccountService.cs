using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace CustomerModule.CustomersRepository
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

        public bool CreateAccount(int customerId)
        {
            try
            {
                using (HttpClient _client = new HttpClient())
                {
                    StringValues token;
                    newHttpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out token);
                    _client.BaseAddress = new Uri(newConfiguration["BaseUrl:Account"]);
                    _client.DefaultRequestHeaders.Add("Authorization", token.ToString());
                    var Stringpayload = JsonConvert.SerializeObject(new { CustomerId = customerId });
                    var payload = new StringContent(Stringpayload, Encoding.UTF8, "application/json");
                    HttpResponseMessage responseMessage = _client.PostAsync($"api/account/createAccount", payload).Result;
                    if (responseMessage.IsSuccessStatusCode)
                        return true;
                    else
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
