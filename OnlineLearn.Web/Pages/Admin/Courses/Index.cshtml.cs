using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLearn.Core.DTOs;
using OnlineLearn.Core.Security;
using OnlineLearn.Core.Services.Interfaces;

namespace OnlineLearn.Web.Pages.Admin.Courses
{
    [PermissionChecker(2)]

    public class IndexModel : PageModel
    {
        private ICourseService _courseService;

        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<ShowCourseForAdminVM> ListCourse { get; set; }
        public void OnGet()
        {
            ListCourse = _courseService.GetCoursesForAdmin();
        }
    }
}
