using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLearn.Core.DTOs;
using OnlineLearn.Core.Services.Interfaces;

namespace OnlineLearn.Web.Pages.Admin.Users
{
    public class DeleteUserModel : PageModel
    {
        private readonly IUserService _userService;

        public DeleteUserModel(IUserService userService)
        {
            _userService = userService;
        }

        public UserInformationVM UserInformationVM { get; set; }
        public void OnGet(int id)
        {
            ViewData["UserId"] = id;
            UserInformationVM = _userService.GetUserInformation(id);
        }

        public IActionResult OnPost(int userId)
        {
            _userService.DeleteUser(userId);
            return RedirectToPage("Index");
        }
    }
}
