namespace EducationHub.Web.ViewModels.Courses
{
    using System.Collections.Generic;

    using Data.Models;
    using Services.Mapping;

    public class DetailsCourseViewModel : IMapFrom<Course>
    {
        public string Title { get; set; }

        public IEnumerable<Lesson> Lessons { get; set; }
    }
}
