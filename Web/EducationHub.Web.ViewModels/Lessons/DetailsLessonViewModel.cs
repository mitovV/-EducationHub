namespace EducationHub.Web.ViewModels.Lessons
{
    using System.Linq;

    using Data.Models;
    using Services.Mapping;

    public class DetailsLessonViewModel : IMapFrom<Lesson>
    {
        public string Title { get; set; }

        public string VideoUrl { get; set; }

        public string ViewModelVideoUrl => this.VideoUrl.Split("/").Reverse().ToArray()[0];
    }
}
