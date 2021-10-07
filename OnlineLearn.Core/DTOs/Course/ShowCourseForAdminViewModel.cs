using OnlineLearn.DataLayer.Entities.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OnlineLearn.Core.DTOs
{
    public class ShowCourseForAdminVM
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public int EpisodeCount { get; set; }
        public List<Course> Courses { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
    //public class CoursesInAdminVM
    //{
    //    public List<Course> Courses { get; set; }
    //    public int CurrentPage { get; set; }
    //    public int PageCount { get; set; }
    //}
}
