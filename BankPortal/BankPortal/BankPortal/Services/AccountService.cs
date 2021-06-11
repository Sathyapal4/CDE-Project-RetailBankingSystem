using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BankPortal.Services
{
    public class AccountService : IAccountService
    {
        private IHttpContextAccessor newHttpContextAccessor;

        public AccountService(IHttpContextAccessor httpContextAccessor)
        {
            newHttpContextAccessor = httpContextAccessor;
        }
        public async Task<HttpResponseMessage> GetAccount(int AccountId)
        {
            using (HttpClient data = new HttpClient())
            {
                //Base URI

                //data.BaseAddress = new Uri("http://localhost:5004");
                data.BaseAddress = new Uri("PUT AZURE DEPLOYED LINK OF ACCOUNT SERVICE HERE");

                string token = newHttpContextAccessor.HttpContext.Session.GetString("Token");
                data.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                data.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/Json"));
                //URI Link Body Part+Add Account Id - {/api/Account/GetAccount/{AccountId}}
                var response = await data.GetAsync("api/Account/GetAccount/" + AccountId);
                return response;
            }
        }

        public async Task<HttpResponseMessage> GetCustomerAccounts(int CustomerId)
        {
            using (HttpClient client = new HttpClient())
            {
                //Base URI
                //client.BaseAddress = new Uri("http://localhost:5004");
                client.BaseAddress = new Uri("PUT AZURE DEPLOYED LINK OF ACCOUNT SERVICE HERE");
                string token = newHttpContextAccessor.HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/Json"));
                //URI Link Body Part+Add CustomerId - {/api/Account/GetCustomerAccounts/{AccountId}}
                var response = await client.GetAsync("api/Account/GetCustomerAccounts/" + CustomerId);
                return response;
            }
        }

        public async Task<HttpResponseMessage> GetAccountStatement(int accountId, string from_date, string to_date)
        {
            using (HttpClient client = new HttpClient())
            {
                //Base URI
                //client.BaseAddress = new Uri("http://localhost:5004");
                client.BaseAddress = new Uri("PUT AZURE DEPLOYED LINK OF ACCOUNT SERVICE HERE");
                string token = newHttpContextAccessor.HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/Json"));
                //URI Link Body Part+Add AccountId - {[action]/{accountId}/{from_date}/{to_date}}
                var response = await client.GetAsync("api/Account/GetStatement/" + accountId + "/" + from_date + "/" + to_date);
                return response;
            }
        }
    }
}
