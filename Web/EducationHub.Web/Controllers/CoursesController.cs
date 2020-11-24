namespace EducationHub.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Courses;

    public class CoursesController : BaseController
    {
        public IActionResult ByCategory()
        {
            return this.View();
        }

        public IActionResult Create()
            => this.View();

        [HttpPost]
        public IActionResult Create(CreateCourseInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            return this.Redirect("/");
        }
    }
}
