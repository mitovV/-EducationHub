namespace EducationHub.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EducationHub.Data.Common.Repositories;
    using EducationHub.Data.Models;
    using EducationHub.Data.Repositories;
    using EducationHub.Services.Data.Comments;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class CommentsServiceTests
    {
        private readonly IList<Comment> comments;
        private readonly Mock<IDeletableEntityRepository<Comment>> mockRepo;
        private readonly ICommentsService commentsService;

        public CommentsServiceTests()
        {
            this.comments = new List<Comment>();
            this.mockRepo = new Mock<IDeletableEntityRepository<Comment>>();
            this.mockRepo.Setup(x => x.AddAsync(It.IsAny<Comment>())).Callback((Comment comment) => this.comments.Add(comment));
            this.mockRepo.Setup(x => x.All()).Returns(this.comments.AsQueryable());
            this.commentsService = new CommentsService(this.mockRepo.Object);
        }

        [Fact]
        public async Task CreateShouldWorkCorrect()
        {
            // Arrange
            var comment = new Comment
            {
                Content = "test",
                ParentId = 1,
                PostId = 12,
                UserId = "test",
            };

            // Act
            await this.commentsService.Create(comment.PostId, comment.UserId, comment.Content);
            var result = this.mockRepo.Object.All().FirstOrDefault();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(this.comments);
        }

        // TODO: Not Complete
        [Fact]
        public async Task IsInPostIdAsyncShouldReturtTrue()
        {
            // Arrange
            var comment = new Comment
            {
                Content = "test",
                ParentId = 1,
                PostId = 12,
                UserId = "test",
            };

            this.mockRepo.Setup(x => x.All()).Returns(this.comments.AsQueryable()).Callback((IQueryable<Comment> comments) => comments.AsNoTracking<Comment>());

            // Act
            await this.commentsService.Create(comment.PostId, comment.UserId, comment.Content);

            var result = await this.commentsService.IsInPostIdAsync(comment.Id, 12);

            // Assert
            Assert.True(result);
        }
    }
}
