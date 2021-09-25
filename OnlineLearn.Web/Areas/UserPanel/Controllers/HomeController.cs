using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearn.Core.DTOs;
using OnlineLearn.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearn.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View(_userService.GetUserInformation(User.Identity.Name));
        }

        #region EditProfile

        [Route("UserPanel/EditProfile")]
        public IActionResult EditProfile()
        {
            return View(_userService.GetUserDataToEditProfile(User.Identity.Name));
        }

        [Route("UserPanel/EditProfile")]
        [HttpPost]
        public IActionResult EditProfile(EditProfileVM profile)
        {
            if (!ModelState.IsValid)
                return View(profile);

            _userService.EditProfile(User.Identity.Name, profile);
            //Log Out User
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/Login?EditProfile=true");
        }
        #endregion

        #region ChangePassword
        [Route("UserPanel/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("UserPanel/ChangePassword")]
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordVM change)
        {
            string currentUserName = User.Identity.Name;
            if (!ModelState.IsValid)
                return View(change);
            if(!_userService.CompareOldPassword(currentUserName,change.OldPassword))
            {
                ModelState.AddModelError("OldPassword", "کلمه عبور فعلی صحیح نمیباشد");
                return View(change);
            }
            _userService.ChangeUserPassword(currentUserName, change.Password);
            ViewBag.IsSuccess = true;

            return View();
        }
        #endregion
    }
}
