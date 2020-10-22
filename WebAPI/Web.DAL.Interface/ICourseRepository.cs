using System;
using System.Collections.Generic;
using System.Text;
using WEB.Domainnn.Response.Course;

namespace Web.DAL.Interface
{
    public interface ICourseRepository
    {
        IEnumerable<CourseView> Gets();
    }
}
