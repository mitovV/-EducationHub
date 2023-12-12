namespace EducationHub.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Services.Data.Lessons;
    using Web.ViewModels.Administration.Lessons;
    using Microsoft.AspNetCore.Mvc;

    public class LessonsController : AdministrationController
    {
        private readonly ILessonsService lessonsService;

        public LessonsController(ILessonsService lessonsService)
        {
            this.lessonsService = lessonsService;
        }

        public async Task<IActionResult> All()
         => this.View(await this.lessonsService.AllWithDeletedAsync<LessonAdminViewModel>());
    }
}
