namespace EducationHub.Services.Data.Tests.Models
{
    using EducationHub.Data.Models;
    using EducationHub.Services.Mapping;

    public class LessonModel : IMapFrom<Lesson>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string VideoUrl { get; set; }

        public string UserId { get; set; }

        public int CategoryId { get; set; }

        public string CourseId { get; set; }
    }
}
