namespace EducationHub.Web.ViewModels.Administration.Courses
{
    using System.Collections.Generic;

    public class PagingCoursesAdminViewModel : PagingViewModel
    {
        public IEnumerable<CourseAdminViewModel> Courses { get; set; }
    }
}
