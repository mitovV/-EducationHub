namespace EducationHub.Web.ViewModels.Lessons
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;

    using AutoMapper;
    using Data.Models;
    using Ganss.XSS;
    using Services.Mapping;

    public class DetailsLessonViewModel : IMapFrom<Lesson>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string VideoUrl { get; set; }

        public string UserUsername { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserPictureUrl { get; set; }

        public string UserId { get; set; }

        public string Description { get; set; }

        public double AverageVote { get; set; }

        public string AvarageVoteAsString => this.AverageVote.ToString("0.0", CultureInfo.InvariantCulture);

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

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Lesson, DetailsLessonViewModel>()
                .ForMember(x => x.AverageVote, y => y.MapFrom(u => u.User.TheyVoted.Count() == 0 ? 0 : u.User.TheyVoted.Average(v => v.Value)));
        }
    }
}
