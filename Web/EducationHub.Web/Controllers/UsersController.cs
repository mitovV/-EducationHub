namespace EducationHub.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Users;
    using Services.Data.Votes;
    using ViewModels.Users;

    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly IVotesService votesService;

        public UsersController(IUsersService usersService, IVotesService votesService)
        {
            this.usersService = usersService;
            this.votesService = votesService;
        }

        public async Task<IActionResult> MyResources()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var viewModel = await this.usersService.GetResourcesInfoAsync<ResourcesViewModel>(userId);

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(string id)
        {
            var viewModel = await this.usersService.GetUserAsync<UserProfileViewModel>(id);
            viewModel.Rating = this.votesService.GetAverageVotes(id);
            viewModel.VotesCount = this.votesService.GetVotesCount(id);

            return this.View(viewModel);
        }
    }
}
