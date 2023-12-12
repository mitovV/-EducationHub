namespace EducationHub.Web.ViewModels.Administration.Lessons
{
    using Data.Common.Models;
    using Services.Mapping;

    using Data.Models;

    public class LessonAdminViewModel : BaseDeletableModel<string>, IMapFrom<Lesson>
    {
        public string Title { get; set; }

        public string VideoUrl { get; set; }

        public string UserUserName { get; set; }

        public string CategoryName { get; set; }

        public string CourseTitle { get; set; }
    }
}
