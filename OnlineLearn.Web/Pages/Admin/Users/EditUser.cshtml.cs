using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLearn.Core.Security;
using OnlineLearn.Core.Services.Interfaces;
using TopLearn.Core.DTOs;

namespace OnlineLearn.Web.Pages.Admin.Users
{
    [PermissionChecker(4)]
    public class EditUserModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;

        public EditUserModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        [BindProperty]
        public EditUserVM EditUserVM { get; set; }
        public void OnGet(int id)
        {
            EditUserVM = _userService.ShowUserInEditMode(id);
            ViewData["Roles"] = _permissionService.GetRoles();
        }

        public IActionResult OnPost(List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
                return Page();
            _userService.EditUserFromAdmin(EditUserVM);

            //Edit Roles
            _permissionService.EditUserRoles(EditUserVM.UserId, SelectedRoles);
            return RedirectToPage("Index");
        }
    }
}
