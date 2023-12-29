namespace EducationHub.Web.ViewModels.Courses
{
    using System.Collections.Generic;

    public class PagingCoursesViewModel : BasePagingViewModel
    {
        public IEnumerable<ByCategoryCourseViewModel> Courses { get; set; }
    }
}
