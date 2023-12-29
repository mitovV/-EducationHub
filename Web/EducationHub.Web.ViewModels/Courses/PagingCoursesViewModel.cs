namespace EducationHub.Web.ViewModels.Courses
{
    using System.Collections.Generic;

    public class PagingCoursesViewModel : PagingViewModel
    {
        public IEnumerable<ByCategoryCourseViewModel> Courses { get; set; }
    }
}
