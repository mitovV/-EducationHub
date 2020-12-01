namespace EducationHub.Web.ViewModels.Courses
{
    using Data.Models;
    using Services.Mapping;

    public class ListingCoursesViewModel : BaseListingResoursesViewModel, IMapFrom<Course>
    {
    }
}
