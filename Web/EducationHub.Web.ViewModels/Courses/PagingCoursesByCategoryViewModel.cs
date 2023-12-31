namespace EducationHub.Web.ViewModels.Courses
{
    using System.Collections.Generic;

    public class PagingCoursesByCategoryViewModel : BasePagingViewModel
    {
        public IEnumerable<ByCategoryCourseViewModel> Courses { get; set; }
    }
}
