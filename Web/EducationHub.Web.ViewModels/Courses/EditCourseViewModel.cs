namespace EducationHub.Web.ViewModels.Courses
{
    using System.Collections.Generic;

    using Data.Models;
    using Services.Mapping;

    public class EditCourseViewModel : IMapFrom<Course>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public User User { get; set; }

        public IEnumerable<LessonInCourseViewModel> Lessons { get; set; }
    }
}
