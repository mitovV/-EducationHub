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

        private Course course;

        public CoursesServiceTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<EducationHubDbContext>()
              .UseInMemoryDatabase("coursesDb");

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

            // Act
            await this.FillData(3);

            var courses = await this.coursesService.AllAsync<CourseModel>();

            var expectedCount = 3;
            var actualCount = courses.Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task GetByUserIdAsyncShouldReturnCorrectValues()
        {
            // Arrange

            // Act
            await this.FillData(3);
            await this.coursesService.CreateAsync(this.course.Title, this.course.Description, "another-user", this.course.CategoryId);

            var courses = await this.coursesService.GetByUserIdAsync<CourseModel>(this.course.UserId);

            var expectedCount = 3;
            var actualCount = courses.Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task GetByCategoryIdAsyncShouldReturnCorrectValues()
        {

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
    }
}
