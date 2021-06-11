
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RetailBankingClient.Models.Account;
using RetailBankingClient.Models.Transaction;
using BankPortal.Models;
using BankPortal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace BankPortal.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IAccountService AccountProvider;
        private readonly ITransactionService TransactionProvider;
        private IHttpContextAccessor newHttpContextAccessor;
        public CustomerController(IAccountService AccountProvider, ITransactionService TransactionProvider, IHttpContextAccessor httpContextAccessor)
        {
            this.AccountProvider = AccountProvider;
            this.TransactionProvider = TransactionProvider;
            newHttpContextAccessor = httpContextAccessor;
        }





        //Get Account Balance of Invidiual Account
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public IActionResult GetAccount()
        {

            var obj = SessionHelper.GetObject<UserRequest>(HttpContext.Session, "CurrentUser");
            if (obj != null)
                return View();
            else
                return View("SessionExpired");
        }


        [HttpPost]
        public async Task<IActionResult> GetAccount(AccountDetails details)
        {
            var obj = SessionHelper.GetObject<UserRequest>(HttpContext.Session, "CurrentUser");
            if (obj != null && obj.Role == Role.Customer)
            {

                AccountDetails model = new AccountDetails();
                try
                {
                    var response = await AccountProvider.GetAccount(details.AccountId);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var JsonContent = await response.Content.ReadAsStringAsync();
                        model = JsonConvert.DeserializeObject<AccountDetails>(JsonContent);
                        return View("AccountBalance", model);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ViewBag.Message = "400 Bad Request Error";
                        return View("CustomError");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        ViewBag.Message = "Zero accounts found for this Account ID";
                        return View("CustomError");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        ViewBag.Message = "500 Internal Server Error";
                        return View("CustomError");
                    }
                    else
                    {
                        ViewBag.Message = "Server Under Maintenance";
                        return View("CustomError");
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = e.Message;
                    return View("CustomError");
                }
            }
            else
                return View("SessionExpired");
        }







        //Get All Customer Accounts-Pass Customer ID as Parameter
        [HttpGet]
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> GetCustomerAccounts(int id)
        {
            var obj = SessionHelper.GetObject<UserRequest>(HttpContext.Session, "CurrentUser");
            if (obj != null)
            {

                List<AccountDetails> AllAccounts = new List<AccountDetails>();
                try
                {

                    var response = await AccountProvider.GetCustomerAccounts(id);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var JsonContent = await response.Content.ReadAsStringAsync();
                        AllAccounts = JsonConvert.DeserializeObject<List<AccountDetails>>(JsonContent);
                        double sum = 0;
                        foreach (var i in AllAccounts)
                        {
                            sum = sum + i.Balance;
                        }
                        ViewBag.TotalBalance = sum;
                        ViewBag.cusId = id;
                        return View(AllAccounts);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ViewBag.Message = "400 Bad Request Error";
                        return View("CustomError");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        ViewBag.Message = "Zero accounts found for this Account ID";
                        return View("CustomError");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        ViewBag.Message = "500 Internal Server Error";
                        return View("CustomError");
                    }
                    else
                    {
                        ViewBag.Message = "Server Under Maintenance";
                        return View("CustomError");
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = e.Message;
                    return View("CustomError");
                }
            }
            else
                return View("SessionExpired");
        }






        //Get Statement of Account Transactions
        [HttpGet]
        public IActionResult GetAccountStatement(int id)
        {
            var obj = SessionHelper.GetObject<UserRequest>(HttpContext.Session, "CurrentUser");

            if (obj != null)
            {
                GetStatement model = new GetStatement { Id = id };
                return View(model);
            }
            else
                return View("SessionExpired");
        }



        [HttpPost]
        public async Task<IActionResult> GetAccountStatement(GetStatement model)
        {

            List<Statement> StatementViews = new List<Statement>();
            var obj = SessionHelper.GetObject<UserRequest>(HttpContext.Session, "CurrentUser");
            if (obj != null)
            {

                try
                {
                    if (model.fromDate.Year == 0001 && model.toDate.Year == 0001)
                    {
                        model.fromDate = DateTime.Now.AddMonths(-1);
                        model.toDate = DateTime.Now;
                    }
                    var response = await AccountProvider.GetAccountStatement(model.Id, model.fromDate.ToString("yyyy-MM-dd"), model.toDate.ToString("yyyy-MM-dd"));
                    if (response.IsSuccessStatusCode)
                    {
                        var JsonContent = await response.Content.ReadAsStringAsync();
                        StatementViews = JsonConvert.DeserializeObject<List<Statement>>(JsonContent);
                        return View("StatementList", StatementViews);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ViewBag.Message = "400 Bad Request Error";
                        return View("CustomError");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        ViewBag.Message = "No Statements Found";
                        return View("CustomError");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        ViewBag.Message = "500 Internal Server Error";
                        return View("CustomError");
                    }
                    else
                    {
                        ViewBag.Message = "Server Under Maintenance";
                        return View("CustomError");
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = e.Message;
                    return View("CustomError");
                }
            }
            else
                return View("SessionExpired");
        }








        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Transfer()
        {

            var obj = SessionHelper.GetObject<UserRequest>(HttpContext.Session, "CurrentUser");
            if (obj != null && obj.Role == Role.Customer)
            {
                int customerId = HttpContext.Session.GetInt32("UserId").Value;
                var response = await AccountProvider.GetCustomerAccounts(customerId);
                if (response.IsSuccessStatusCode)
                {
                    var accounts = JsonConvert.DeserializeObject<List<AccountDetails>>(await response.Content.ReadAsStringAsync());
                    List<int> accountIds = accounts.Select(accounts => accounts.AccountId).ToList();
                    ViewBag.AccountIds = accountIds;
                    return View();
                }
                return View("SessionExpired");
            }
            else
                return View("SessionExpired");
        }
        [HttpPost]
        public async Task<IActionResult> Transfer(Transfer transfer)
        {
            var obj = SessionHelper.GetObject<UserRequest>(HttpContext.Session, "CurrentUser");
            if (obj != null && obj.Role == Role.Customer)
            {

                Ref_Transaction_Status ref_Transaction_Status = new Ref_Transaction_Status();
                try
                {
                    var response = await TransactionProvider.Transfer(transfer);
                    if (response.IsSuccessStatusCode)
                    {
                        ref_Transaction_Status = JsonConvert.DeserializeObject<Ref_Transaction_Status>(await response.Content.ReadAsStringAsync());
                        return RedirectToAction("RefTransactionStatus", ref_Transaction_Status);

                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ViewBag.Message = "400 Bad Request Error";
                        return View("CustomError");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        ViewBag.Message = "Zero Accounts found for this Account ID";
                        return View("CustomError");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        ViewBag.Message = "500 Internal Server Error";
                        return View("CustomError");
                    }
                    else
                    {
                        ViewBag.Message = "Server Under Maintenance";
                        return View("CustomError");
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = e.Message;
                    return View("CustomError");

                }
            }
            else
            {
                return View("SessionExpired");
            }
        }

        public IActionResult RefTransactionStatus(Ref_Transaction_Status status)
        {

            var obj = SessionHelper.GetObject<UserRequest>(HttpContext.Session, "CurrentUser");
            if (obj != null && obj.Role == Role.Customer)
                return View(status);
            else
            {
                return View("SessionExpired");
            }
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Withdraw(bool success = false)
        {
            ViewBag.Success = success;

            var obj = SessionHelper.GetObject<UserRequest>(HttpContext.Session, "CurrentUser");

            if (obj != null && obj.Role == Role.Customer)
            {
                int customerId = HttpContext.Session.GetInt32("UserId").Value;
                var response = await AccountProvider.GetCustomerAccounts(customerId);
                if (response.IsSuccessStatusCode)
                {
                    var accounts = JsonConvert.DeserializeObject<List<AccountDetails>>(await response.Content.ReadAsStringAsync());
                    List<int> accountIds = accounts.Select(accounts => accounts.AccountId).ToList();
                    ViewBag.AccountIds = accountIds;
                    return View();
                }
                return View("SessionExpired");

            }
            else
                return View("SessionExpired");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost]
        public async Task<IActionResult> Withdraw(RetailBankingClient.Models.Transaction.Account withdraw)
        {
            TransactionStatus status = new TransactionStatus();

            var obj = SessionHelper.GetObject<UserRequest>(HttpContext.Session, "CurrentUser");
            if (obj != null && obj.Role == Role.Customer)
            {

                try
                {

                    var response = await TransactionProvider.Withdraw(withdraw);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = JsonConvert.DeserializeObject<RetailBankingClient.Models.Transaction.Account>(await response.Content.ReadAsStringAsync());
                        AccountDetails details = new AccountDetails();
                        details.AccountId = withdraw.AccountId;
                        ViewBag.Success = true;
                        return await GetAccount(details);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ViewBag.Message = "Amount Not Debited";
                        return View("CustomError");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        ViewBag.Message = "404 Error";
                        return View("CustomError");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        ViewBag.Message = "500 Internal Server Error";
                        return View("CustomError");
                    }
                    else
                    {
                        ViewBag.Message = "Server Under Maintenance";
                        return View("CustomError");
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = e.Message;
                    return View("CustomError");
                }
            }
            else
                return View("SessionExpired");
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public IActionResult Deposit(bool success = false)
        {
            ViewBag.Success = success;
            var obj = SessionHelper.GetObject<UserRequest>(HttpContext.Session, "CurrentUser");
            if (obj != null && obj.Role == Role.Customer)
                return View();
            else
                return View("SessionExpired");
        }

        [HttpPost]
        public async Task<IActionResult> Deposit(RetailBankingClient.Models.Transaction.Account deposit)
        {
            var obj = SessionHelper.GetObject<UserRequest>(HttpContext.Session, "CurrentUser");
            if (obj != null && obj.Role == Role.Customer)
            {

                try
                {

                    var response = await TransactionProvider.Deposit(deposit);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = JsonConvert.DeserializeObject<RetailBankingClient.Models.Transaction.Account>(await response.Content.ReadAsStringAsync());
                        AccountDetails details = new AccountDetails();
                        details.AccountId = result.AccountId;
                        return RedirectToAction(actionName: "Deposit", controllerName: "Customer", new { success = true });
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ViewBag.Message = "Amount Not Deposited";
                        return View("CustomError");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        ViewBag.Message = "404 Error";
                        return View("CustomError");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        ViewBag.Message = "500 Internal Server Error";
                        return View("CustomError");
                    }
                    else
                    {
                        ViewBag.Message = "Server Under Maintenance";
                        return View("CustomError");
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = e.Message;
                    return View("CustomError");
                }
            }
            else
            {

                return View("SessionExpired");

            }
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public async Task<IActionResult> ViewTransactions(int id)
        {
            List<Financial_Transaction> list = new List<Financial_Transaction>();
            var obj = SessionHelper.GetObject<UserRequest>(HttpContext.Session, "CurrentUser");
            if (obj != null)
            {
                try
                {
                    var response = await TransactionProvider.GetTransactions(id);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var JsonContent = await response.Content.ReadAsStringAsync();
                        list = JsonConvert.DeserializeObject<List<Financial_Transaction>>(JsonContent);
                        return View(list);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        ViewBag.Message = "Server issue while adding the record";
                        return View("CustomError");
                    }

                }
                catch (Exception e)
                {
                    ViewBag.Message = e.Message;
                    return View("CustomError");
                }

            }

            else
                return View("SessionExpired");

            return View(list);



        }
    }
}