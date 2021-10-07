using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineLearn.Core.DTOs;
using OnlineLearn.DataLayer.Entities.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearn.Core.Services.Interfaces
{
    public interface ICourseService
    {
        #region Groups
        List<CourseGroup> GetAllGroups();
        List<SelectListItem> GetGroupsToManageCourse();
        List<SelectListItem> GetSubGroupsToManageCourse(int groupId);
        List<SelectListItem> GetTeachers();
        List<SelectListItem> GetLevels();
        List<SelectListItem> GetStatus();
        #endregion

        #region Courses
        int AddCourse(Course course, IFormFile courseImg, IFormFile courseDemo);
        List<ShowCourseForAdminVM> GetCoursesForAdmin(int pageId = 1, string filterCourse = "");
        #endregion
    }
}
