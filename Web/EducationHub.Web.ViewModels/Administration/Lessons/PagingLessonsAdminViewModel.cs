namespace EducationHub.Web.ViewModels.Administration.Lessons
{
    using System.Collections.Generic;

    public class PagingLessonsAdminViewModel : BasePagingViewModel
    {
        public IEnumerable<NotRelatedLessonAdminViewModel> Lessons { get; set; }
    }
}
