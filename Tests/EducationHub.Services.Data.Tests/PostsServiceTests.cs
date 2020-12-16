namespace EducationHub.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using EducationHub.Data;
    using EducationHub.Data.Models;
    using EducationHub.Data.Repositories;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Posts;
    using Xunit;

    public class PostsServiceTests : IDisposable
    {
        private readonly EfRepository<Post> postsRepository;
        private readonly IPostsService postsService;

        public PostsServiceTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<EducationHubDbContext>()
               .UseInMemoryDatabase("test");

            var dbCntext = new EducationHubDbContext(optionsBuilder.Options);
            dbCntext.Database.EnsureDeleted();
            dbCntext.Database.EnsureCreated();

            this.postsRepository = new EfRepository<Post>(dbCntext);

            this.postsService = new PostsService(this.postsRepository);

            AutoMapperConfig.RegisterMappings(typeof(PostModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task CratePostAsyncShouldWorkCorrect()
        {
            // Arrange

            // Act
            await this.postsService.CratePostAsync("Test", "Test", "1", 1);
            var expectedCount = 1;
            var actualCount = this.postsRepository.All().Count();

            var expectedTitle = "Test";
            var actualTitle = this.postsRepository.All().FirstOrDefault().Title;

            // Assert
            Assert.Equal(expectedCount, actualCount);
            Assert.Equal(expectedTitle, actualTitle);
        }

        [Fact]
        public async Task GetByIdAsyncShluldReturnCorrectValue()
        {
            // Arrange
            var title = "test";
            var content = "test";

            // Act
            await this.postsService.CratePostAsync(title, content, "1", 2);
            var postId = this.postsRepository.All().FirstOrDefault().Id;

            var post = await this.postsService.GetByIdAsync<PostModel>(postId);

            // Assert
            Assert.NotNull(post);
            Assert.Equal(title, post.Title);
            Assert.Equal(content, post.Content);
        }

        // TODO: Not work
        [Fact]
        public async Task GetPostsAsyncShouldReturnCorrectValues()
        {
            // Arrange
            var title = "test";
            var content = "test";

            // Act
            await this.postsService.CratePostAsync(title, content, "1", 2);
            await this.postsService.CratePostAsync(title, content, "1", 2);

            var posts = await this.postsService.GetPostsAsync<PostsModel>(2);

            // Assert

        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool value)
        {
            this.postsRepository.Dispose();
        }
    }
}
