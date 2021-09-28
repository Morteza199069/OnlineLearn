using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLearn.Core.Services.Interfaces;
using TopLearn.Core.DTOs;

namespace OnlineLearn.Web.Pages.Admin.Users
{
    public class DeletedUserListModel : PageModel
    {
        private readonly IUserService _userService;

        public DeletedUserListModel(IUserService userService)
        {
            _userService = userService;
        }
        public UsersInAdminVM UsersInAdminVM { get; set; }
        public void OnGet(int pageId = 1, string filterEmail = "", string filterUsername = "")
        {
            UsersInAdminVM = _userService.GetDeletedUsers(pageId, filterEmail, filterUsername);
        }
    }
}
