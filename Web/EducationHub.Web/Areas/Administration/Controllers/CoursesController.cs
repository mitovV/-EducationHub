namespace EducationHub.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    using EducationHub.Services.Data.Lessons;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Courses;
    using ViewModels.Administration.Courses;

    public class CoursesController : AdministrationController
    {
        private readonly ICoursesService coursesService;
        private readonly ILessonsService lessonsService;

        public CoursesController(ICoursesService coursesService)
        {
            this.coursesService = coursesService;
        }

        public async Task<IActionResult> All()
            => this.View(await this.coursesService.AllWithDeletedAsync<CourseAdminViewModel>());

        public IActionResult Create()
        {
            return this.RedirectToActionPermanent("Create", "Courses", new { area = string.Empty });
        }

        public async Task<IActionResult> Edit(string id)
        {
            var viewModel = await this.coursesService.GetByIdWithDeletedAsync<AdminEditCourseViewModel>(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

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
            if (this.coursesService.IfExist(id))
            {
                var lesoonsInCourse = this.lessonsService.DeleteAllInCourseAsync(id);
                await this.coursesService.DeleteAsync(id);
            }

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
