namespace EducationHub.Web.ViewModels.Courses
{
    using System.Collections.Generic;

    public class PagingCoursesByUserViewModel : BasePagingViewModel
    {
        public IEnumerable<ListingCoursesViewModel> Courses { get; set; }

    }
}
