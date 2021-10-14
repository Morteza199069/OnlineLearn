using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineLearn.Core.Security;
using OnlineLearn.Core.Services.Interfaces;
using OnlineLearn.DataLayer.Entities.Course;

namespace OnlineLearn.Web.Pages.Admin.Courses
{
    [PermissionChecker(4)]

    public class EditCourseModel : PageModel
    {
        private readonly ICourseService _courseService;

        public EditCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public Course Course { get; set; }
        public void OnGet(int id)
        {
            Course = _courseService.GetCourseById(id);
            var groups = _courseService.GetGroupsToManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text", Course.GroupId);

            var subGroups = _courseService.GetSubGroupsToManageCourse(Course.GroupId);
            ViewData["SubGroups"] = new SelectList(subGroups, "Value", "Text", Course.SubGroup ?? 0);

            var teachers = _courseService.GetTeachers();
            ViewData["Teachers"] = new SelectList(teachers, "Value", "Text", Course.TeacherId);

            var levels = _courseService.GetLevels();
            ViewData["Levels"] = new SelectList(levels, "Value", "Text", Course.LevelId);

            var statues = _courseService.GetStatus();
            ViewData["Status"] = new SelectList(statues, "Value", "Text", Course.StatusId);
        }

        public IActionResult OnPost(IFormFile imgCourseUp, IFormFile demoUp)
        {
            if (!ModelState.IsValid)
                return Page();

            _courseService.UpdateCourse(Course, imgCourseUp, demoUp);

            return RedirectToPage("Index");
        }
    }
}
