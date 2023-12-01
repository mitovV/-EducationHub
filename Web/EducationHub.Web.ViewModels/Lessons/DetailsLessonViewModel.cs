namespace EducationHub.Web.ViewModels.Lessons
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    using AutoMapper;
    using Data.Models;
    using Ganss.Xss;
    using Services.Mapping;
    using Users;

    public class DetailsLessonViewModel : UserBadgeViewModel, IMapFrom<Lesson>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string VideoUrl { get; set; }

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

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Lesson, DetailsLessonViewModel>()
                .ForMember(x => x.Username, y => y.MapFrom(l => l.User.UserName))
                .ForMember(x => x.PictureUrl, y => y.MapFrom(l => l.User.PictureUrl))
                .ForMember(x => x.Id, y => y.MapFrom(l => l.User.Id))
                .ForMember(x => x.Votes, y => y.MapFrom(l => l.User.TheyVoted.Count()))
                .ForMember(x => x.AverageVote, y => y.MapFrom(l => l.User.TheyVoted.Count() == 0 ? 0 : l.User.TheyVoted.Average(v => v.Value)));
        }
    }
}
