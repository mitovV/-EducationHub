namespace EducationHub.Web.ViewModels.Administration.Courses
{
    using Data.Common.Models;
    using Data.Models;
    using Services.Mapping;

    public class CourseAdminViewModel : BaseDeletableModel<string>, IMapFrom<Course>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string UserUserName { get; set; }

        public string CategoryName { get; set; }
    }
}
