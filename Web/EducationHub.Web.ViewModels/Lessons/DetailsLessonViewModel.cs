namespace EducationHub.Web.ViewModels.Lessons
{
    using System.Linq;

    using EducationHub.Data.Models;
    using EducationHub.Services.Mapping;

    public class DetailsLessonViewModel : IMapFrom<Lesson>
    {
        public string Title { get; set; }

        public string VideoUrl { get; set; }

        public string ViewModelVideo => this.VideoUrl.Split("/").Reverse().ToArray()[0];
    }
}
