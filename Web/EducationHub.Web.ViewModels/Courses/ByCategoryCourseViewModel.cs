namespace EducationHub.Web.ViewModels.Courses
{
    using System;

    using Data.Models;
    using Ganss.Xss;
    using Services.Mapping;

    public class ByCategoryCourseViewModel : IMapFrom<Course>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Description);

        public string ShortTitle => this.Title.Length > 18 ? this.Title.Substring(0, 18) + "..." : this.Title;

        public string UserUsername { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
