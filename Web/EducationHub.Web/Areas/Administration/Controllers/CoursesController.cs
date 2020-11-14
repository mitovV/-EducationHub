namespace EducationHub.Web.Areas.Administration.Controllers
{
    using EducationHub.Services.Data.Categories;
    using EducationHub.Services.Data.Courses;
    using Microsoft.AspNetCore.Mvc;

    public class CoursesController : AdministrationController
    {
        private readonly ICoursesService coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            this.coursesService = coursesService;
        }

        public IActionResult All()
            => this.View(this.coursesService.All());
    }
}
