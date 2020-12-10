namespace EducationHub.Services.Data.Comments
{
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task Create(int postId, string userId, string content, int? parentId = null);

        Task<bool> IsInPostIdAsync(int commentId, int postId);
    }
}
