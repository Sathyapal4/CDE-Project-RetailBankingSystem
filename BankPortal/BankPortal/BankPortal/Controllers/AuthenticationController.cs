using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BankPortal.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BankPortal.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: AuthenticationController
        [HttpGet]
        public IActionResult Index()
        {
            var obj = SessionHelper.GetObject<UserRequest>(HttpContext.Session, "CurrentUser");
            if (obj != null) return RedirectToAction("HomePage");
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IFormCollection form)
        {
            Role roleval;

            if (form["role"] == 0.ToString())
                roleval = Role.Employee;
            else
                roleval = Role.Customer;

            UserRequest userRequest = new UserRequest()
            {
                Email = form["mail"],
                Password = form["pass"],
                Role = roleval
            };

            //SessionHelper.SetObject(HttpContext.Session, "CurrentUser", userRequest);

            //call for microsservice

            if (!ModelState.IsValid)
                return View("Error");

            using (HttpClient client = new HttpClient())
            {
                //steps
                //1.Set baseaddress
                //  2.seriallize the object into jsonstring
                //  3. put that jsong string json object
                //  4.call the microservice

                //client.BaseAddress = new Uri("http://localhost:5001");
                client.BaseAddress = new Uri("PUT AZURE DEPLOYED LINK OF AUTHENTICATION MODULE");
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer " + token);
                var jsonstring = JsonConvert.SerializeObject(userRequest);
                var obj = new StringContent(jsonstring, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/Authentication/Login", obj);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    UserResponse resobj = JsonConvert.DeserializeObject<UserResponse>(response.Content.ReadAsStringAsync().Result);
                    HttpContext.Session.SetString("Token", resobj.Token);//Set
                    string token = HttpContext.Session.GetString("Token");//Get Token

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                    if (resobj.Token != null)
                    {
                        HttpContext.Session.SetInt32("UserId", resobj.Id);
                        HttpContext.Session.SetString("Message", resobj.Message);
                        SessionHelper.SetObject(HttpContext.Session, "CurrentUser", userRequest);
                        //Token
                        return RedirectToAction("HomePage", "Authentication");
                    }
                    else
                    {
                        ViewBag.Message = "Token Expired";
                        return View("CustomError");
                    }
                }

                else if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    ModelState.AddModelError("", "Bad credential.");
                    ViewBag.Message = "Bad credential.";
                    return View("CustomError");
                }
                else
                {


                    return View("Invalid");
                }

            }

        }




        public IActionResult HomePage()
        {
            var obj = SessionHelper.GetObject<UserRequest>(HttpContext.Session, "CurrentUser");
            if (obj != null)
            {
                ViewBag.Id = HttpContext.Session.GetInt32("UserId");
                return View(SessionHelper.GetObject<UserRequest>(HttpContext.Session, "CurrentUser"));
            }
            else
                return View("Index");

        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove("CurrentUser");
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }


    }
}
