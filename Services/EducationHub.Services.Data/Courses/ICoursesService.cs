namespace EducationHub.Services.Data.Courses
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Web.ViewModels.Administration;

    public interface ICoursesService
    {
        Task<IEnumerable<CourseAdminViewModel>> AllAsync();
    }
}
