namespace EducationHub.Web.ViewModels.Administration.Courses
{
    using System.Collections.Generic;

    public class PagingCoursesAdminViewModel : BasePagingViewModel
    {
        public IEnumerable<CourseAdminViewModel> Courses { get; set; }
    }
}
