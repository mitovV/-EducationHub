namespace EducationHub.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Courses;
    using ViewModels.Administration;

    public class CoursesController : AdministrationController
    {
        private readonly ICoursesService coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            this.coursesService = coursesService;
        }

        public IActionResult All()
            => this.View(this.coursesService.AllAsync<CategoryAdminViewModel>());
    }
}
