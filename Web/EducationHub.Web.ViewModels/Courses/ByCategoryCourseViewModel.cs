namespace EducationHub.Web.ViewModels.Courses
{
    using System;

    using Data.Models;
    using Services.Mapping;

    public class ByCategoryCourseViewModel : IMapFrom<Course>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string UserUsername { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
