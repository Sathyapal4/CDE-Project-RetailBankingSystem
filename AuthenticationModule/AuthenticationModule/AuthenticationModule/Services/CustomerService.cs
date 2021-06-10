using AuthenticationModule.Models;
using log4net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace AuthenticationModule.AuthenticationsRepository
{
    public class CustomerService : ICustomerService
    {
        private readonly IConfiguration newConfiguration;


        public CustomerService(IConfiguration configuration)
        {
            newConfiguration = configuration;
        }

        public UserResponse CheckUser(UserRequest userRequest)
        {
            try
            {
                using (HttpClient _client = new HttpClient())
                {
                    _client.BaseAddress = new Uri(newConfiguration["BaseUrl:Customer"]);
                    var payload = new StringContent(JsonConvert.SerializeObject(userRequest), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseMessage = _client.PostAsync("api/customers/checkCredentials", payload).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var response = JsonConvert.DeserializeObject<UserResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                        return response;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }


        }
    }
}
