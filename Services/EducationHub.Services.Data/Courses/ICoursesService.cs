namespace EducationHub.Services.Data.Courses
{
    using System.Collections.Generic;

    using Web.ViewModels.Administration;

    public interface ICoursesService
    {
        IEnumerable<CourseAdminViewModel> All();
    }
}
