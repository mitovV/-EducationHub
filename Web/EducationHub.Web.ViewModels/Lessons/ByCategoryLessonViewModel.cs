namespace EducationHub.Web.ViewModels.Lessons
{
    using Courses;
    using Data.Models;
    using Services.Mapping;

    public class ByCategoryLessonViewModel : ByCategoryCourseViewModel, IMapFrom<Lesson>
    {
    }
}
