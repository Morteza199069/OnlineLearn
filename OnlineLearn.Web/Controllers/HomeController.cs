using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearn.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearn.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index() => View();

        [Route("onlinePayment/{id}")]
        public IActionResult onlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];
                var wallet = _userService.GetWalletByWalletId(id);

                var payment = new ZarinpalSandbox.Payment(wallet.Amount);
                var response = payment.Verification(authority).Result;
                if(response.Status==100)
                {
                    ViewBag.code = response.RefId;
                    ViewBag.IsSuccess = true;
                    wallet.IsPay = true;
                    _userService.UpdateWallet(wallet);
                }
            }

            return View();
        }
    }
}

