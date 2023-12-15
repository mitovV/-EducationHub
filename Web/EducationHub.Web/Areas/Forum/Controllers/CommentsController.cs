namespace EducationHub.Web.Areas.Forum.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Comments;
    using ViewModels.Forum.Comments;

    public class CommentsController : ForumController
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        public async Task<IActionResult> Create(CreateCommentInputModel input)
        {
            var parentId =
                input.ParentId == 0 ?
                    (int?)null :
                    input.ParentId;

            if (parentId.HasValue)
            {
                var isInPost = await this.commentsService.IsInPostIdAsync(parentId.Value, input.PostId);

                if (!isInPost)
                {
                    return this.BadRequest();
                }
            }

            if (!this.ModelState.IsValid)
            {
                return this.Redirect(this.Request.Headers.Referer.ToString());
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.commentsService.Create(input.PostId, userId, input.Content, parentId);

            return this.RedirectToAction("Details", "Posts", new { id = input.PostId });
        }
    }
}
