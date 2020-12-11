namespace EducationHub.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Courses;
    using ViewModels.Administration.Courses;

    public class CoursesController : AdministrationController
    {
        private readonly ICoursesService coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            this.coursesService = coursesService;
        }

        public async Task<IActionResult> All()
            => this.View(await this.coursesService.AllWithDeletedAsync<CourseAdminViewModel>());

        public async Task<IActionResult> Edit(string id)
        {
            var viewModel = await this.coursesService.GetByIdAsync<AdminEditCourseViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminEditCourseViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.coursesService.EditAsync(model.Id, model.Title, model.Description, model.IsDeleted, model.CategoryId);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.coursesService.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
