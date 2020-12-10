namespace EducationHub.Services.Data.Comments
{
    using System.Linq;
    using System.Threading.Tasks;

    using EducationHub.Data.Common.Repositories;
    using EducationHub.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task Create(int postId, string userId, string content, int? parentId = null)
        {
            var comment = new Comment
            {
                Content = content,
                ParentId = parentId,
                PostId = postId,
                UserId = userId,
            };
            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public async Task<bool> IsInPostIdAsync(int commentId, int postId)
        {
            var commentPostId = await this.commentsRepository
                .All()
                .Where(x => x.Id == commentId)
                .Select(x => x.PostId)
                .FirstOrDefaultAsync();

            return commentPostId == postId;
        }
    }
}
