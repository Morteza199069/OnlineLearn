using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearn.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace OnlineLearn.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IOrderService _orderService;

        public CourseController(ICourseService courseService, IOrderService orderService)
        {
            _courseService = courseService;
            _orderService = orderService;
        }
        public IActionResult Index(int pageId = 1, int take = 0, string filter = "", string getType = "all",
            string orderByType = "date", int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null)
        {
            ViewBag.pageId = pageId;
            ViewBag.selectedGroups = selectedGroups;
            ViewBag.Groups = _courseService.GetAllGroups();
            return View(_courseService.GetCourses(pageId, 9, filter, getType, orderByType, startPrice, endPrice, selectedGroups));
        }

        [Route("ShowCourse/{id}")]
        public IActionResult ShowCourse(int id)
        {
            var course = _courseService.GetCourseDetails(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [Authorize]
        public IActionResult BuyCourse(int id)
        {
            int orderId = _orderService.AddOrder(User.Identity.Name, id);
            return Redirect("/UserPanel/MyOrders/ShowOrder/" + orderId);
        }

        [Route("FileDownload/{episodeId}")]
        public IActionResult FileDownload(int episodeId)
        {
            var episode = _courseService.GetEpisodeById(episodeId);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles", episode.EpisodeFileName);
            string fileName = episode.EpisodeFileName;
            if(episode.IsFree)
            {
                byte[] file = System.IO.File.ReadAllBytes(filePath);
                return File(file, "application/force-download", fileName);
            }
            if(User.Identity.IsAuthenticated)
            {
                if(_orderService.IsUserInCourse(User.Identity.Name,episodeId))
                {
                    byte[] file = System.IO.File.ReadAllBytes(filePath);
                    return File(file, "application/force-download", fileName);
                }
            }

            return Forbid();
        }
    }
}
