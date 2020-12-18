namespace EducationHub.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EducationHub.Data;
    using EducationHub.Data.Common.Repositories;
    using EducationHub.Data.Models;
    using EducationHub.Data.Repositories;
    using EducationHub.Services.Data.Comments;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class CommentsServiceTests : IDisposable
    {
        private readonly EfDeletableEntityRepository<Comment> commentsRepository;
        private readonly CommentsService commentsService;
        private readonly Comment comment;

        public CommentsServiceTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<EducationHubDbContext>()
             .UseInMemoryDatabase("commentsDb");

            var dbCntext = new EducationHubDbContext(optionsBuilder.Options);
            dbCntext.Database.EnsureDeleted();
            dbCntext.Database.EnsureCreated();

            this.commentsRepository = new EfDeletableEntityRepository<Comment>(dbCntext);

            this.commentsService = new CommentsService(this.commentsRepository);

            this.comment = new Comment
            {
                Content = "test",
                ParentId = 1,
                PostId = 12,
                UserId = "test",
            };
        }

        [Fact]
        public async Task CreateShouldWorkCorrect()
        {
            // Arrange
            var comments = new List<Comment>();
            var mockRepo = new Mock<IDeletableEntityRepository<Comment>>();
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Comment>())).Callback((Comment comment) => comments.Add(comment));
            mockRepo.Setup(x => x.All()).Returns(comments.AsQueryable());

            var commentsService = new CommentsService(mockRepo.Object);

            // Act
            await commentsService.Create(this.comment.PostId, this.comment.UserId, this.comment.Content);
            var result = mockRepo.Object.All().FirstOrDefault();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(comments);
        }

        [Fact]
        public async Task IsInPostIdAsyncShouldReturtTrue()
        {
            // Arrange

            // Act
            await this.commentsService.Create(this.comment.PostId, this.comment.UserId, this.comment.Content);
            var commentId = this.commentsRepository.All().FirstOrDefault().Id;
            var result = await this.commentsService.IsInPostIdAsync(commentId, this.comment.PostId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task IsInPostIdAsyncShouldReturtFalse()
        {
            // Arrange

            // Act
            await this.commentsService.Create(this.comment.PostId, this.comment.UserId, this.comment.Content);
            var commentId = this.commentsRepository.All().FirstOrDefault().Id;
            var result = await this.commentsService.IsInPostIdAsync(commentId, 14);

            // Assert
            Assert.False(result);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool value)
        {
            this.commentsRepository.Dispose();
        }
    }
}
