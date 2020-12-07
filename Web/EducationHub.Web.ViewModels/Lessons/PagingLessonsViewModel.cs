namespace EducationHub.Web.ViewModels.Lessons
{
    using System.Collections.Generic;

    public class PagingLessonsViewModel : PagingViewModel
    {
        public int CategoryId { get; set; }

        public IEnumerable<ByCategoryLessonViewModel> Lessons { get; set; }
    }
}
