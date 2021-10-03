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
    }
}
