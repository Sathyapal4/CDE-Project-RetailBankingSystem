using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using BankPortal.Models;
using BankPortal.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankPortal.Controllers
{
    public class EmployeeController : Controller
    {
       
        IAccountService newAccountservice;
        ICustomerService newProvider;
        IConfiguration newConfig;
        
        public EmployeeController( IAccountService accountService, ICustomerService provider, IConfiguration config)
        {
          
            newAccountservice = accountService;
            newProvider = provider;
            newConfig = config;
        }

        //public EmployeeController()
        //{
        //}


        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public async Task<IActionResult> ViewCustomerAccountDetails(int id)
        {
            

            //Microservice
            Customer customer;
            List<Account> list;
            CustomerAccountViewModel model = new CustomerAccountViewModel();

            var obj = SessionHelper.GetObject<UserRequest>(HttpContext.Session, "CurrentUser");
            if (obj != null && obj.Role == Role.Employee)
            {
                customer = new Customer();
            list = new List<Account>();

            try
            {
                var response = await newProvider.GetCustomerDetails(id);
                if (response.IsSuccessStatusCode)
                {
                    var JsonContent = await response.Content.ReadAsStringAsync();
                    customer = JsonConvert.DeserializeObject<Customer>(JsonContent);
                    //account account micro service to get listof accounts


                    var result = await newAccountservice.GetCustomerAccounts(id);

                    list = JsonConvert.DeserializeObject<List<Account>>(await result.Content.ReadAsStringAsync());

                    model.Customer = customer;
                    model.AccountDetails = list;
                    return View(model);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ViewBag.Message = "No any record Found! Bad Request";
                    return View("CustomError");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    ViewBag.Message = "Having server issue while adding record";
                    return View("CustomError");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    ViewBag.Message = "No record found in DB for ID :" + id;
                    return View("CustomError");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            }
            else
                return View("UnAuthorized");

            return View(model);

        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public async Task<IActionResult> ViewCustomers()
        {

            List<Customer> customers;

           var obj = SessionHelper.GetObject<UserRequest>(HttpContext.Session, "CurrentUser");
            if (obj != null && obj.Role == Role.Employee)
            {
                customers = new List<Customer>();
                try
                {
                    var response = await newProvider.GetCustomers();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var JsonContent = await response.Content.ReadAsStringAsync();
                        customers = JsonConvert.DeserializeObject<List<Customer>>(JsonContent);
                        return View(customers);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        ViewBag.Message = "Having Server error ";
                        return View("CustomError");
                    }

                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View("CustomError");
                }
            }
            else
            {
                return View("SessionExpired");
            }
            return View(customers);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public async Task<IActionResult> CreateCustomer(bool success=false)
        {
            ViewBag.Success = success;

            var obj = SessionHelper.GetObject<UserRequest>(HttpContext.Session, "CurrentUser");
            if (obj != null && obj.Role == Role.Employee)
                return  View();
            else
                return View("UnAuthorized");

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {


            var obj = SessionHelper.GetObject<UserRequest>(HttpContext.Session, "CurrentUser");
            if (obj != null && obj.Role == Role.Employee)
            {

                try
            {
                if(!ModelState.IsValid)
                {
                    

                    return View("CreateCustomer",customer);
                }
               

                var response = await newProvider.CreateCustomer(customer);
                if (response.IsSuccessStatusCode)
                {
                    var jsoncontent = await response.Content.ReadAsStringAsync();

                    return RedirectToAction("CreateCustomer", new { success = true });
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    ModelState.AddModelError("", "Having server issue while adding record");
                    ViewBag.Message = "Having server issue while adding record";
                    return View("CustomError");

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    ModelState.AddModelError("", "Username already present with ID :" + customer.CustomerId);
                    ViewBag.Message = "Username already present with ID: " + customer.CustomerId;

                    return View("CustomError");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ModelState.AddModelError("", "Invalid model states");
                    ViewBag.Message = "InValid Model States";
                    return View("CustomError");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("CustomError");
            }
            }
            else
            {
                return View("UnAuthorized");
            }
            return RedirectToAction("CreateCustomer");
        }
       




    }


}
//}



