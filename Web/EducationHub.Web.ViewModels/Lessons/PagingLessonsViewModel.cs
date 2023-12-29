namespace EducationHub.Web.ViewModels.Lessons
{
    using System.Collections.Generic;

    public class PagingLessonsViewModel : BasePagingViewModel
    {
        public IEnumerable<ByCategoryLessonViewModel> Lessons { get; set; }
    }
}
