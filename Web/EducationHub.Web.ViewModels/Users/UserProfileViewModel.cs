namespace EducationHub.Web.ViewModels.Users
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using AutoMapper;
    using Data.Models;
    using Services.Mapping;
    using ViewModels.Courses;
    using ViewModels.Lessons;

    public class UserProfileViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        private const int ResoursesCount = 5;

        public string UserName { get; set; }

        public string PictureUrl { get; set; }

        public double Rating { get; set; }

        public int VotesCount { get; set; }

        public string RatingAsString => this.Rating.ToString("0.0", CultureInfo.InvariantCulture);

        public IEnumerable<ListingCoursesViewModel> Courses { get; set; }

        public IEnumerable<ListingLessonsViewModel> Lessons { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<User, UserProfileViewModel>()
                .ForMember(x => x.Courses, y => y.MapFrom(u => u.Courses.OrderByDescending(c => c.CreatedOn).Take(ResoursesCount)))
                .ForMember(x => x.Lessons, y => y.MapFrom(u => u.Lessons.Where(l => l.CourseId == null).OrderByDescending(c => c.CreatedOn).Take(ResoursesCount)));
        }
    }
}
