﻿using Microsoft.AspNetCore.Mvc;
using OnlineLearn.Core.Convertors;
using OnlineLearn.Core.DTOs;
using OnlineLearn.Core.Genetrator;
using OnlineLearn.Core.Security;
using OnlineLearn.Core.Services.Interfaces;
using OnlineLearn.DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearn.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #region Registration

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterVM register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            if (_userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری معتبر نمی باشد");
                return View(register);
            }
            if (_userService.IsExistEmail(FixedText.FixEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "ایمیل معتبر نمی باشد");
                return View(register);
            }

            User user = new User()
            {
                ActiveCode = NameGenerator.GenerateUniqueCode(),
                Email = FixedText.FixEmail(register.Email),
                IsActive = false,
                Password = PasswordHelper.EncodePasswordMd5(register.Password),
                UserAvatar = "Default.jpg",
                RegisterDate = DateTime.Now,
                UserName = register.UserName
            };
            _userService.AddUser(user);

            #region Send Activation Email
            #endregion
            return View("SuccessRegister", user);
        }
        #endregion

        #region User Login
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginVM login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            var user = _userService.LoginUser(login);
            if (user != null)
            {
                if (user.IsActive)
                {
                    //Login
                    ViewBag.IsSuccess = true;
                    return View();
                }
                else
                {
                    ModelState.AddModelError("Email", "حساب کاربری شما فعال نمی باشد");
                }
            }
            ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
            return View(login);
        }
        #endregion

        #region Active Account
        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = _userService.ActiveAccount(id);
            return View();
        }
        #endregion
    }
}