namespace EducationHub.Web.ViewModels.Courses
{
    using System.Collections.Generic;

    public class PagingCoursesViewModel : PagingViewModel
    {
        public int CategoryId { get; set; }

        public IEnumerable<ByCategoryCourseViewModel> Courses { get; set; }
    }
}
