namespace EducationHub.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Courses;
    using EducationHub.Data;
    using EducationHub.Data.Common.Repositories;
    using EducationHub.Data.Models;
    using EducationHub.Data.Repositories;
    using EducationHub.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Xunit;

    public class CoursesServiceTests : IDisposable
    {
        private readonly IDeletableEntityRepository<Course> coursesRepository;
        private readonly ICoursesService coursesService;

        private readonly Course course;

        public CoursesServiceTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<EducationHubDbContext>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbCntext = new EducationHubDbContext(optionsBuilder.Options);
            dbCntext.Database.EnsureDeleted();
            dbCntext.Database.EnsureCreated();

            this.coursesRepository = new EfDeletableEntityRepository<Course>(dbCntext);

            this.coursesService = new CoursesService(this.coursesRepository);

            this.course = new Course
            {
                CategoryId = 1,
                Title = "test",
                Description = "test",
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

            var course = await this.coursesRepository.All().FirstOrDefaultAsync();

            var expectedTitle = this.course.Title;
            var actualTitle = course.Title;

            var expectedDescription = this.course.Description;
            var actualDescription = course.Description;

            var expectedUserId = this.course.UserId;
            var actualUserId = course.UserId;

            var expectedCategoryId = this.course.CategoryId;
            var actualCategoryId = course.CategoryId;

            // Assert
            Assert.Equal(expectedTitle, actualTitle);
            Assert.Equal(expectedDescription, actualDescription);
            Assert.Equal(expectedUserId, actualUserId);
            Assert.Equal(expectedCategoryId, actualCategoryId);
        }

        [Fact]
        public async Task AllAsyncShouldReturnCorrectValues()
        {
            // Arrange
            var expectedCount = 3;

            // Act
            await this.FillData(expectedCount);

            var courses = await this.coursesService.AllAsync<CourseModel>();

            var actualCount = courses.Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task GetByUserIdAsyncShouldReturnCorrectValues()
        {
            // Arrange
            var expectedCount = 3;

            // Act
            await this.FillData(expectedCount);
            await this.coursesService.CreateAsync(this.course.Title, this.course.Description, "another-user", this.course.CategoryId);

            var courses = await this.coursesService.GetByUserIdAsync<CourseModel>(this.course.UserId);

            var actualCount = courses.Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task GetByCategoryIdAsyncShouldReturnCorrectValues()
        {
            // Arrange
            var expectedCount = 2;

            // Act
            await this.FillData(expectedCount);
            await this.coursesService.CreateAsync(this.course.Title, this.course.Description, this.course.UserId, 3);

            var courses = await this.coursesService.GetByCategoryIdAsync<CourseModel>(this.course.CategoryId, 1, 10);

            var actualCount = courses.Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task GetByCategoryIdAsyncShouldReturnDataInCorrectOrder()
        {
            // Arrange
            var expectedTitle = "Last";

            // Act
            await this.FillData(3);
            await this.coursesService.CreateAsync(expectedTitle, this.course.Description, this.course.UserId, this.course.CategoryId);

            var courses = await this.coursesService.GetByCategoryIdAsync<CourseModel>(this.course.CategoryId, 1);

            var actualTitle = courses.FirstOrDefault().Title;

            // Assert
            Assert.Equal(expectedTitle, actualTitle);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(3)]
        [InlineData(8)]
        public async Task GetByCategoryIdAsyncShouldReturnCorrectNumberOfItemsPerPages(int itemsPerPage)
        {
            // Arrange
            var expectedCount = itemsPerPage;

            // Act
            this.EnsureRepositoryIsEmpty();
            await this.FillData(itemsPerPage + 1);

            var courses = await this.coursesService.GetByCategoryIdAsync<CourseModel>(this.course.CategoryId, 1, itemsPerPage);

            var actualCount = courses.Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task GetByCategoryIdAsyncShouldReturnCorrectNumberOfPage()
        {
            // Arrange
            var expectedTitle = "Second page";
            var expectedCoursesCount = 1;

            // Act
            await this.coursesService.CreateAsync(expectedTitle, this.course.Description, this.course.UserId, this.course.CategoryId);
            await this.FillData(4);

            var courses = await this.coursesService.GetByCategoryIdAsync<CourseModel>(this.course.CategoryId, 2);

            var actualTitle = courses.FirstOrDefault().Title;
            var actualCoursesCount = courses.Count();

            // Assert
            Assert.Equal(expectedTitle, actualTitle);
            Assert.Equal(expectedCoursesCount, actualCoursesCount);
        }

        [Fact]
        public async Task GetByIdAsyncShouldReturnCorrectValues()
        {
            // Arrange

            // Act
            await this.FillData(1);

            var courseId = this.coursesRepository.All().FirstOrDefault().Id;

            var course = await this.coursesService.GetByIdAsync<CourseModel>(courseId);

            // Assert
            Assert.Equal(this.course.Title, course.Title);
            Assert.Equal(this.course.Description, course.Description);
            Assert.Equal(this.course.UserId, course.UserId);
            Assert.Equal(this.course.CategoryId, course.CategoryId);
        }

        [Fact]
        public async Task GetByIdAsyncShouldReturnNull()
        {
            // Arrange

            // Act
            var course = await this.coursesService.GetByIdAsync<CourseModel>("test");

            // Assert
            Assert.Null(course);
        }

        [Fact]
        public async Task DeleteAsyncShouldWorkCorrect()
        {
            // Arrange

            // Act
            await this.FillData(2);

            var lessonid = this.coursesRepository.All().FirstOrDefault().Id;

            await this.coursesService.DeleteAsync(lessonid);

            var expectedCount = 1;
            var actualCount = this.coursesRepository.All().Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task GetCountByCategoryShouldReturnCorrectValue()
        {
            // Arrange
            var expectedCount = 3;

            // Act
            await this.FillData(expectedCount);

            var actualCount = this.coursesService.GetCountByCategory(this.course.CategoryId);

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task EditAsyncShouldWorkCorrect()
        {
            // Arrange
            var expectedTitle = "Changed";

            // Act
            await this.FillData(1);

            var course = this.coursesRepository.All().FirstOrDefault();

            await this.coursesService.EditAsync(course.Id, expectedTitle, course.Description, course.IsDeleted, course.CategoryId);

            var actualTitle = course.Title;

            // Assert
            Assert.Equal(expectedTitle, actualTitle);
        }

        [Fact]
        public async Task AllWithDeletedAsyncShouldReturnCorrectValues()
        {
            // Arrange
            var expectedCount = 2;

            // Act
            await this.FillData(expectedCount);

            var lessonid = this.coursesRepository.All().FirstOrDefault().Id;

            await this.coursesService.DeleteAsync(lessonid);

            var courses = await this.coursesService.AllWithDeletedAsync<CourseModel>();

            var actualCount = courses.Count();

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
            this.coursesRepository.Dispose();
        }

        private async Task FillData(int count)
        {
            for (int i = 0; i < count; i++)
            {
                await this.coursesService.CreateAsync(this.course.Title, this.course.Description, this.course.UserId, this.course.CategoryId);
            }
        }

        private void EnsureRepositoryIsEmpty()
        {
            var courses = this.coursesRepository.All();

            foreach (var course in courses)
            {
                this.coursesRepository.Delete(course);
            }
        }
    }
}
