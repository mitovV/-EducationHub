namespace EducationHub.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CoursesController : BaseController
    {
        public IActionResult ByCategory()
        {
            return this.View();
        }
    }
}
