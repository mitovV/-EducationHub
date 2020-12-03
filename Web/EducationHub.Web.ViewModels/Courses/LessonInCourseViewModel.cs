namespace EducationHub.Web.ViewModels.Courses
{
    using Data.Models;
    using Services.Mapping;

    public class LessonInCourseViewModel : IMapFrom<Lesson>
    {
        public string Id { get; set; }

        public string Title { get; set; }
    }
}
