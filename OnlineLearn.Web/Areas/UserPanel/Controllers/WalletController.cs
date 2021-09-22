using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearn.Core.DTOs.User;
using OnlineLearn.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearn.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class WalletController : Controller
    {
        private readonly IUserService _userService;

        public WalletController(IUserService userService)
        {
            _userService = userService;
        }
        [Route("UserPanel/Wallet")]
        public IActionResult Index()
        {
            ViewBag.WalletList = _userService.GetUserWallet(User.Identity.Name);
            return View();
        }

        [Route("UserPanel/Wallet")]
        [HttpPost]
        public IActionResult Index(ChargeWalletVM charge)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.WalletList = _userService.GetUserWallet(User.Identity.Name);
                return View(charge);
            }

            _userService.ChargeWallet(User.Identity.Name, charge.Amount, "شارژ حساب");

            // online payment

            return null;
        }
    }
}
