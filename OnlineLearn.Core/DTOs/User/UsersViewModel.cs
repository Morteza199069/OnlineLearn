﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using OnlineLearn.DataLayer.Entities.User;

namespace TopLearn.Core.DTOs
{
    public class UsersInAdminVM
    {
        public List<User> Users { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }

    public class CreateUserVM
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }

        public IFormFile UserAvatar { get; set; }
    }
}
