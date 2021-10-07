using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineLearn.Core.Services.Interfaces;
using OnlineLearn.DataLayer.Entities.Course;

namespace OnlineLearn.Web.Pages.Admin.Courses
{
    public class CreateCourseModel : PageModel
    {
        private readonly ICourseService _courseService;

        public CreateCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public Course Course { get; set; }
        public void OnGet()
        {
            var groups = _courseService.GetGroupsToManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text");

            var subGroups = _courseService.GetSubGroupsToManageCourse(int.Parse(groups.First().Value));
            ViewData["SubGroups"] = new SelectList(subGroups, "Value", "Text");

            var teachers = _courseService.GetTeachers();
            ViewData["Teachers"] = new SelectList(teachers, "Value", "Text");

            var levels = _courseService.GetLevels();
            ViewData["Levels"] = new SelectList(levels, "Value", "Text");

            var statues = _courseService.GetStatus();
            ViewData["Status"] = new SelectList(statues, "Value", "Text");

        }
    }
}
