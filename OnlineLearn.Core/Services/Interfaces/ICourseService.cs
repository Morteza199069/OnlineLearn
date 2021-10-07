using Microsoft.AspNetCore.Mvc.Rendering;
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
        List<CourseGroup> GetAllGroups();
        List<SelectListItem> GetGroupsToManageCourse();
        List<SelectListItem> GetSubGroupsToManageCourse(int groupId);
        List<SelectListItem> GetTeachers();
        List<SelectListItem> GetLevels();
        List<SelectListItem> GetStatus();
    }
}
