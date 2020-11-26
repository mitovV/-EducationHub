namespace EducationHub.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Users;
    using Web.ViewModels.Users;

    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public async Task<IActionResult> MyResources()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var viewModel = await this.usersService.GetResourcesInfoAsync<ResourcesViewModel>(userId);

            return this.View(viewModel);
        }
    }
}
