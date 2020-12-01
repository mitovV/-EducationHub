namespace EducationHub.Web.ViewModels.Courses
{
    using Data.Models;
    using Services.Mapping;

    public class EditCourseViewModel : IMapFrom<Course>
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
