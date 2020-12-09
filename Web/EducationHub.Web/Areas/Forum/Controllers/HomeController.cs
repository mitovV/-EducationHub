namespace EducationHub.Web.Areas.Forum.Controllers
{
    using System.Threading.Tasks;

    using Services.Data;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : ForumController
    {
        private readonly IGetCountsService getCountsService;

        public HomeController(IGetCountsService getCountsService)
        {
            this.getCountsService = getCountsService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await this.getCountsService.GetForumPostsCountsAsync();

            return this.View(viewModel);
        }
    }
}
