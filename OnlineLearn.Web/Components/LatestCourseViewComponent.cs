using Microsoft.AspNetCore.Mvc;
using OnlineLearn.Core.Services.Interfaces;
using System.Threading.Tasks;

namespace OnlineLearn.Web.Components
{
    public class LatestCourseViewComponent:ViewComponent
    {
        private readonly ICourseService _courseService;

        public LatestCourseViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("LatestCourse", _courseService.GetCourses().Item1));
        }
    }
}
