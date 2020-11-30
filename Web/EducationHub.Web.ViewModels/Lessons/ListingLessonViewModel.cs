namespace EducationHub.Web.ViewModels.Lessons
{
    using System;

    using Data.Models;
    using Services.Mapping;

    public class ListingLessonViewModel : IMapFrom<Lesson>
    {
        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
