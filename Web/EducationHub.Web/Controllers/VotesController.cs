namespace EducationHub.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Votes;
    using ViewModels.Votes;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : BaseController
    {
        private readonly IVotesService votesService;

        public VotesController(IVotesService votesService)
        {
            this.votesService = votesService;
        }

        [HttpPost]
        public async Task<ActionResult<PostVoteResponseModel>> Post(PostVoteInputModel model)
        {
            var votedId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.votesService.SetVoteAsync(votedId, model.VotedForId, model.Value);

            var averageVote = this.votesService.GetAverageVotes(model.VotedForId);

            return new PostVoteResponseModel
            {
                AverageVote = averageVote,
            };
        }
    }
}
