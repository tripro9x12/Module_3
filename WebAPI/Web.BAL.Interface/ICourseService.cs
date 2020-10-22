using WEB.Domainnn.Response.Course;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Web.BAL.Interface
{
    public interface ICourseService
    {
        IEnumerable<CourseView> Gets();
    }
}
