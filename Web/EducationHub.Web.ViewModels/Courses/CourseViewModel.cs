namespace EducationHub.Web.ViewModels.Courses
{
    using Data.Models;
    using Services.Mapping;

    public class CourseViewModel : IMapFrom<Course>
    {
        public string CategoryName { get; set; }

        public string Title { get; set; }

        public int CategoryId { get; set; }

        public string UserId { get; set; }
    }
}
