namespace EducationHub.Web.ViewModels.Lessons
{
    using System.Collections.Generic;

    public class PagingLessonsViewModel : PagingViewModel
    {
        public IEnumerable<ByCategoryLessonViewModel> Lessons { get; set; }
    }
}
