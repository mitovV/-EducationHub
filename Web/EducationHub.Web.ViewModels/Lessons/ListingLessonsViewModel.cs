namespace EducationHub.Web.ViewModels.Lessons
{
    using Data.Models;
    using Services.Mapping;

    public class ListingLessonsViewModel : BaseListingResoursesViewModel, IMapFrom<Lesson>
    {
    }
}
