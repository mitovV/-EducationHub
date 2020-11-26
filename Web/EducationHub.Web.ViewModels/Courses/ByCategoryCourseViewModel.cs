namespace EducationHub.Web.ViewModels.Courses
{
    using System;

    using Data.Models;
    using Ganss.XSS;
    using Services.Mapping;

    public class ByCategoryCourseViewModel : IMapFrom<Course>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Description);

        public string UserUsername { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
