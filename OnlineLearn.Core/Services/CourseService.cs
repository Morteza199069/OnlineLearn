using OnlineLearn.Core.Services.Interfaces;
using OnlineLearn.DataLayer.Context;
using OnlineLearn.DataLayer.Entities.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearn.Core.Services
{
    public class CourseService: ICourseService
    {
        private readonly OnlineLearnContext _context;

        public CourseService(OnlineLearnContext context)
        {
            _context = context;
        }

        public List<CourseGroup> GetAllGroups()
        {
            return _context.CourseGroups.ToList();
        }
    }
}
