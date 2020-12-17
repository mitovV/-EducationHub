namespace EducationHub.Services.Data.Tests.Models
{
    using EducationHub.Data.Models;
    using EducationHub.Services.Mapping;

    public class CourseModel : IMapFrom<Course>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public int CategoryId { get; set; }
    }
}
