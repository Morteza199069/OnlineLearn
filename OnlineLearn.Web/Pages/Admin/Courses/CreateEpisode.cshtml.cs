using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLearn.Core.Security;
using OnlineLearn.Core.Services.Interfaces;
using OnlineLearn.DataLayer.Entities.Course;

namespace OnlineLearn.Web.Pages.Admin.Courses
{
    [PermissionChecker(7)]
    public class CreateEpisodeModel : PageModel
    {
        private ICourseService _courseService;

        public CreateEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseEpisode CourseEpisode { get; set; }
        public void OnGet(int id)
        {
            CourseEpisode = new CourseEpisode();
            CourseEpisode.CourseId = id;
        }

        public IActionResult OnPost(IFormFile episodeFile)
        {
            if (!ModelState.IsValid || episodeFile == null)
                return Page();

            if(_courseService.CheckFileExist(episodeFile.FileName))
            {
                ViewData["IsExistFile"] = true;
                return Page();
            }

            _courseService.AddEpisode(CourseEpisode, episodeFile);
            return Redirect("/Admin/Courses/IndexEpisode/" + CourseEpisode.CourseId);
        }
    }
}
