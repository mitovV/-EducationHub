namespace EducationHub.Services.Data.Tests
{
    using System;
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
        private const string Title = "Test";
        private const string Content = "Test";
        private const string UserId = "Test";
        private const int CategoryId = 1;

        private readonly EfRepository<Post> postsRepository;
        private readonly IPostsService postsService;

        public PostsServiceTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<EducationHubDbContext>()
               .UseInMemoryDatabase("postsDb");

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
            var expectedCount = 1;

            // Act
            await this.FillData(expectedCount);

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

            // Act
            await this.FillData(1);
            var postId = this.postsRepository.All().FirstOrDefault().Id;

            var post = await this.postsService.GetByIdAsync<PostModel>(postId);

            // Assert
            Assert.NotNull(post);
            Assert.Equal(Title, post.Title);
            Assert.Equal(Content, post.Content);
        }

        [Fact]
        public async Task GetByIdAsyncShluldReturnNull()
        {
            // Arrange

            // Act
            var post = await this.postsService.GetByIdAsync<PostModel>(1);

            // Assert
            Assert.Null(post);
        }

        [Fact]
        public async Task GetPostsAsyncShouldReturnCorrectValues()
        {
            // Arrange
            var secondTitle = "test2";
            var secondContent = "test2";

            // Act
            await this.FillData(1);
            await this.postsService.CratePostAsync(secondTitle, secondContent, UserId, CategoryId);

            var expectedCount = 2;

            var posts = await this.postsService.GetPostsAsync<PostModel>(expectedCount);

            var actualCount = posts.Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task GetPostsAsyncShouldReturnDataInCorrectOrder()
        {
            // Arrange
            var secondTitle = "test2";
            var secondContent = "test2";

            // Act
            await this.FillData(1);
            await this.postsService.CratePostAsync(secondTitle, secondContent, UserId, CategoryId);

            var posts = await this.postsService.GetPostsAsync<PostModel>(2);

            var expectedTitle = "test2";
            var actualTitle = posts.FirstOrDefault().Title;

            // Assert
            Assert.Equal(expectedTitle, actualTitle);
        }

        [Fact]
        public async Task GetPostsByCategoryAsyncShouldReturtCorrectValues()
        {
            // Arrange
            var secondTitle = "test2";
            var secondContent = "test2";

            var thirdTitle = "test3";
            var thirdContent = "test3";

            // Act
            await this.FillData(1);
            await this.postsService.CratePostAsync(secondTitle, secondContent, UserId, CategoryId);
            await this.postsService.CratePostAsync(thirdTitle, thirdContent, UserId, 3);

            var posts = await this.postsService.GetPostsByCategoryAsync<PostModel>(CategoryId);
            var expectedCount = 2;
            var actualCount = posts.Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
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

        private async Task FillData(int count)
        {
            for (int i = 0; i < count; i++)
            {
                await this.postsService.CratePostAsync(Title, Content, UserId, CategoryId);
            }
        }
    }
}
