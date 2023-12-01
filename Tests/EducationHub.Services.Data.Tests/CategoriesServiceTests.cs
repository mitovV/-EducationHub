namespace EducationHub.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Categories;
    using EducationHub.Data;
    using EducationHub.Data.Models;
    using EducationHub.Data.Repositories;
    using EducationHub.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Xunit;

    public class CategoriesServiceTests : IDisposable
    {
        private readonly EfDeletableEntityRepository<Category> categoriesRepository;
        private readonly ICategoriesService categoriesService;
        private readonly Category category;

        public CategoriesServiceTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<EducationHubDbContext>()
             .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbCntext = new EducationHubDbContext(optionsBuilder.Options);
            dbCntext.Database.EnsureDeleted();
            dbCntext.Database.EnsureCreated();

            this.categoriesRepository = new EfDeletableEntityRepository<Category>(dbCntext);

            this.categoriesService = new CategoriesService(this.categoriesRepository);

            this.category = new Category
            {
                Name = "Test",
                PictureUrl = "Test",
                UserId = "test",
            };

            AutoMapperConfig.RegisterMappings(typeof(PostModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task CreateAsyncShouldWorkCorrect()
        {
            // Arrange

            // Act
            await this.FillData(1);
            var category = await this.categoriesRepository.All().FirstOrDefaultAsync();

            var expectedName = this.category.Name;
            var expectedPictureUrl = this.category.PictureUrl;
            var expectedUserId = this.category.UserId;

            var actualName = category.Name;
            var actualPictureUrl = category.PictureUrl;
            var actualUserId = category.UserId;

            // Assert
            Assert.NotNull(category);
            Assert.Equal(expectedName, actualName);
            Assert.Equal(expectedPictureUrl, actualPictureUrl);
            Assert.Equal(expectedUserId, actualUserId);
        }

        [Fact]
        public async Task AllAsyncShouldReturnCorrectValues()
        {
            // Arrange
            var expectedCount = 3;

            // Act
            await this.FillData(expectedCount);
            await this.categoriesService.CreateAsync("to delete", "test", "test");

            var toDelete = this.categoriesRepository.All().LastOrDefault().Id;
            await this.categoriesService.DeleteAsync(toDelete);

            var categories = await this.categoriesService.AllAsync<CategoryModel>();

            var actualCount = categories.Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task AllWithDeletedAsyncShouldReturnCorrectValues()
        {
            // Arrange
            var expectedCount = 4;

            // Act
            await this.FillData(expectedCount);

            var categoryId = this.categoriesRepository.All().FirstOrDefault().Id;

            await this.categoriesService.DeleteAsync(categoryId);
            var categories = await this.categoriesService.AllWithDeletedAsync<CategoryModel>();

            var actualCount = categories.Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task GetByIdAsyncShouldReturnCorrectValue()
        {
            // Arrange
            var category = new Category
            {
                Name = "Category to get",
                PictureUrl = "Test",
                UserId = "test",
            };

            // Act
            await this.FillData(3);
            await this.categoriesService.CreateAsync(category.Name, category.PictureUrl, category.UserId);

            var categoryId = this.categoriesRepository
                .All()
                .Where(x => x.Name == category.Name)
                .FirstOrDefault().Id;

            var result = await this.categoriesService.GetByIdAsync<CategoryModel>(categoryId);

            // Assert
            Assert.Equal(categoryId, result.Id);
            Assert.Equal(category.Name, result.Name);
            Assert.Equal(category.PictureUrl, result.PictureUrl);
            Assert.Equal(category.UserId, result.UserId);
        }

        [Fact]
        public async Task GetByIdAsyncShouldReturnCorrectValueWhenEntityIsDeleted()
        {
            // Arrange
            var category = new Category
            {
                Name = "To delete",
                PictureUrl = "Test",
                UserId = "test",
            };

            // Act
            await this.FillData(3);
            await this.categoriesService.CreateAsync(category.Name, category.PictureUrl, category.UserId);

            var categoryId = this.categoriesRepository
                .All()
                .Where(x => x.Name == category.Name)
                .FirstOrDefault().Id;

            await this.categoriesService.DeleteAsync(categoryId);

            var result = await this.categoriesService.GetByIdAsync<CategoryModel>(categoryId);

            // Assert
            Assert.Equal(categoryId, result.Id);
            Assert.Equal(category.Name, result.Name);
            Assert.Equal(category.PictureUrl, result.PictureUrl);
            Assert.Equal(category.UserId, result.UserId);
        }

        [Fact]
        public async Task EditAsyncShouldWorkCorrect()
        {
            // Arrange
            var expectedName = "Changed name";
            var expectedPictureUrl = "Changed Picture Url";

            // Act
            await this.FillData(1);

            var categoryId = this.categoriesRepository.All().FirstOrDefault().Id;

            await this.categoriesService.EditAsync(categoryId, expectedName, expectedPictureUrl, false, this.category.UserId);

            var category = await this.categoriesService.GetByIdAsync<CategoryModel>(categoryId);

            var actualName = category.Name;
            var actualPictureUrl = category.PictureUrl;
            var actualId = category.Id;

            // Assert
            Assert.Equal(expectedName, actualName);
            Assert.Equal(expectedPictureUrl, actualPictureUrl);
            Assert.Equal(categoryId, actualId);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool value)
        {
            this.categoriesRepository.Dispose();
        }

        private async Task FillData(int count)
        {
            for (int i = 0; i < count; i++)
            {
                await this.categoriesService.CreateAsync(this.category.Name, this.category.PictureUrl, this.category.UserId);
            }
        }
    }
}
