namespace EducationHub.Web.ViewModels.Lessons
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Data.Models;
    using Ganss.XSS;
    using Services.Mapping;

    public class DetailsLessonViewModel : IMapFrom<Lesson>
    {
        public string Title { get; set; }

        public string VideoUrl { get; set; }

        public string UserUsername { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserPictureUrl { get; set; }

        public string Description { get; set; }

        public string SanitiedDescription => new HtmlSanitizer().Sanitize(this.Description);

        public string ViewModelVideoUrl
        {
            get
            {
                if (this.VideoUrl.Contains("v="))
                {
                    return new Regex("v=[^&]+&").Match(this.VideoUrl).Value.TrimStart('v', '=').TrimEnd('&');
                }
                else
                {
                    return this.VideoUrl.Split("/").Reverse().ToArray()[0];
                }
            }
        }
    }
}
