using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using BankPortal.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BankPortal.Services
{
    public class CustomerService : ICustomerService
    {
        private IHttpContextAccessor newHttpContextAccessor;

        public CustomerService(IHttpContextAccessor httpContextAccessor)
        {
            newHttpContextAccessor = httpContextAccessor;
        }

       
        public async Task<HttpResponseMessage> CreateCustomer(Customer model)
        {


            using (HttpClient client = new HttpClient())
            {
                string token = newHttpContextAccessor.HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //client.BaseAddress = new Uri("http://localhost:5002");
                client.BaseAddress = new Uri("PUT AZURE DEPLOYED LINK OF CUSTOMER SERVICE HERE");
                var jsonstring = JsonConvert.SerializeObject(model);
                var obj = new StringContent(jsonstring, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/Customers/createCustomer", obj);
                return response;
            }
        }
        
        public async Task<HttpResponseMessage> GetCustomerDetails(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string token = newHttpContextAccessor.HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //client.BaseAddress = new Uri("http://localhost:5002");
                client.BaseAddress = new Uri("PUT AZURE DEPLOYED LINK OF CUSTOMER SERVICE HERE");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/Json"));

                var response = await client.GetAsync("api/Customers/getCustomerDetails/" + id);
                return response;
            }
        }

        
        public async Task<HttpResponseMessage> GetCustomers()
        {
            using (HttpClient client = new HttpClient())
            {
                //client.BaseAddress = new Uri("http://localhost:5002");
                client.BaseAddress = new Uri("PUT AZURE DEPLOYED LINK OF CUSTOMER SERVICE HERE");
                string token = newHttpContextAccessor.HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/Json"));
                var response = await client.GetAsync("api/Customers/GetAllCustomers");
                return response;
            }
        }
    }
}
