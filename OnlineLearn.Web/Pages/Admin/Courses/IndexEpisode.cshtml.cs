using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLearn.Core.Security;
using OnlineLearn.Core.Services.Interfaces;
using OnlineLearn.DataLayer.Entities.Course;

namespace OnlineLearn.Web.Pages.Admin.Courses
{
    [PermissionChecker(6)]

    public class IndexEpisodeModel : PageModel
    {
        private ICourseService _courseService;

        public IndexEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<CourseEpisode> CourseEpisodes { get; set; }
        public void OnGet(int id)
        {
            ViewData["CourseId"] = id;
            CourseEpisodes = _courseService.GetCourseEpisodesList(id);
        }
    }
}
