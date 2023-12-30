namespace EducationHub.Web.ViewModels.Lessons
{
    using System.Collections.Generic;

    public class PagingLessonsByUserViewModel : BasePagingViewModel
    {
        public IEnumerable<ListingLessonsViewModel> Lessons { get; set; }
    }
}
