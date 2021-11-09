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
        List<ShowCourseForAdminVM> GetCoursesForAdmin();
        int AddCourse(Course course, IFormFile imgCourse, IFormFile courseDemo);
        Course GetCourseById(int courseId);
        void UpdateCourse(Course course, IFormFile imgCourse, IFormFile courseDemo);
        Tuple<List<ShowCourseListItemViewModel>,int> GetCourses(int pageId = 1, int take = 0, string filter = "", string getType = "all",
            string orderByType = "date", int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null);
        Course GetCourseDetails(int courseId);
        #endregion

        #region Episodes
        int AddEpisode(CourseEpisode episode, IFormFile episodeFile);
        CourseEpisode GetEpisodeById(int episodeId);
        void EditEpisode(CourseEpisode episode, IFormFile episodeFile);
        List<CourseEpisode> GetCourseEpisodesList(int courseId);
        bool CheckFileExist(string fileName);

        #endregion

        #region Comments
        void AddComment(CourseComment comment);
        Tuple<List<CourseComment>, int> GetCourseComment(int courseId, int pageId= 1);
        #endregion
    }
}
