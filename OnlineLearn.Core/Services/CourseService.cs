using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineLearn.Core.Convertors;
using OnlineLearn.Core.DTOs;
using OnlineLearn.Core.Genetrator;
using OnlineLearn.Core.Security;
using OnlineLearn.Core.Services.Interfaces;
using OnlineLearn.DataLayer.Context;
using OnlineLearn.DataLayer.Entities.Course;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearn.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly OnlineLearnContext _context;

        public CourseService(OnlineLearnContext context)
        {
            _context = context;
        }

        public int AddCourse(Course course, IFormFile courseImg, IFormFile courseDemo)
        {
            course.CreateDate = DateTime.Now;
            course.CourseImageName = "no-photo.jpg";

            //Image Check
            if (courseImg != null && courseImg.IsImage())
            {
                course.CourseImageName = NameGenerator.GenerateUniqueCode() + Path.GetExtension(courseImg.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/image", course.CourseImageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    courseImg.CopyTo(stream);
                }

                //Image Resize 
                ImageConvertor imageConvertor = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/thumb", course.CourseImageName);
                imageConvertor.Image_resize(imagePath, thumbPath, 150);
            }

            //Upload Demo
            if (courseDemo != null)
            {
                course.DemoFileName = NameGenerator.GenerateUniqueCode() + Path.GetExtension(courseDemo.FileName);
                string demoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/demos", course.DemoFileName);
                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    courseDemo.CopyTo(stream);
                }
            }
            _context.Add(course);
            _context.SaveChanges();

            return course.CourseId;
        }

        public List<CourseGroup> GetAllGroups()
        {
            return _context.CourseGroups.ToList();
        }

        public List<ShowCourseForAdminVM> GetCoursesForAdmin(int pageId = 1, string filterCourse = "")
        {
            IQueryable<Course> result = _context.Courses;
            if (!string.IsNullOrEmpty(filterCourse))
            {
                result = result.Where(c => c.CourseTitle.Contains(filterCourse));
            }
            int take = 20;
            int skip = (pageId - 1) * take;
            return _context.Courses.Select(c => new ShowCourseForAdminVM()
            {
                CourseId = c.CourseId,
                ImageName = c.CourseImageName,
                Title = c.CourseTitle,
                EpisodeCount = c.CourseEpisodes.Count,
                CurrentPage = pageId,
                PageCount = result.Count() / take,
                Courses = result.OrderBy(c => c.CreateDate).Skip(skip).Take(take).ToList()
            }).ToList();
        }

    public List<SelectListItem> GetGroupsToManageCourse()
    {
        return _context.CourseGroups.Where(g => g.ParentId == null)
            .Select(g => new SelectListItem()
            {
                Text = g.GroupTitle,
                Value = g.GroupId.ToString()
            }).ToList();
    }

    public List<SelectListItem> GetLevels()
    {
        return _context.CourseLevels.Select(l => new SelectListItem()
        {
            Value = l.LevelId.ToString(),
            Text = l.LevelTitle
        }).ToList();
    }

    public List<SelectListItem> GetStatus()
    {
        return _context.CourseStatuses.Select(s => new SelectListItem()
        {
            Value = s.StatusId.ToString(),
            Text = s.StatusTitle
        }).ToList();
    }

    public List<SelectListItem> GetSubGroupsToManageCourse(int groupId)
    {
        return _context.CourseGroups.Where(g => g.ParentId == groupId)
            .Select(g => new SelectListItem()
            {
                Text = g.GroupTitle,
                Value = g.GroupId.ToString()
            }).ToList();
    }

    public List<SelectListItem> GetTeachers()
    {
        return _context.UserRoles.Where(r => r.RoleId == 4).Include(r => r.User)
            .Select(u => new SelectListItem()
            {
                Text = u.User.UserName,
                Value = u.UserId.ToString()
            }).ToList();
    }
}
}
