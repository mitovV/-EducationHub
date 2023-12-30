namespace EducationHub.Web.ViewModels.Lessons
{
    using System.Collections.Generic;

    public class PagingLessonsByCategoryViewModel : BasePagingViewModel
    {
        public IEnumerable<ByCategoryLessonViewModel> Lessons { get; set; }
    }
}
