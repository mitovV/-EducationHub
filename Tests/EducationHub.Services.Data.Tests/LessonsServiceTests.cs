namespace EducationHub.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using EducationHub.Data;
    using EducationHub.Data.Common.Repositories;
    using EducationHub.Data.Models;
    using EducationHub.Data.Repositories;
    using Lessons;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Xunit;

    public class LessonsServiceTests : IDisposable
    {
        private readonly IDeletableEntityRepository<Lesson> lessonsRepository;
        private readonly ILessonsService lessonsService;

        private Lesson lesson;

        public LessonsServiceTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<EducationHubDbContext>()
              .UseInMemoryDatabase("lessonsDb");

            var dbCntext = new EducationHubDbContext(optionsBuilder.Options);
            dbCntext.Database.EnsureDeleted();
            dbCntext.Database.EnsureCreated();

            this.lessonsRepository = new EfDeletableEntityRepository<Lesson>(dbCntext);

            this.lessonsService = new LessonsService(this.lessonsRepository);

            this.lesson = new Lesson
            {
                Title = "Test",
                Description = "Test",
                VideoUrl = "Test",
                UserId = "Test",
                CategoryId = 1,
            };

            AutoMapperConfig.RegisterMappings(typeof(PostModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task CreateAsyncWithoutCourseShuldWorkCorrect()
        {
            // Arrange

            // Act
            await this.FillData(1);

            var lesson = await this.lessonsRepository.All().FirstOrDefaultAsync();

            var expectedTitle = this.lesson.Title;
            var actualTitle = lesson.Title;

            var expectedDescription = this.lesson.Description;
            var actualDescription = lesson.Description;

            var expectedVideoUrl = this.lesson.VideoUrl;
            var actualVideoUrl = lesson.VideoUrl;

            var expectedUserId = this.lesson.UserId;
            var actualUserId = lesson.UserId;

            var expectedCategoryId = this.lesson.CategoryId;
            var actualCategoryId = lesson.CategoryId;

            // Assert
            Assert.Equal(expectedTitle, actualTitle);
            Assert.Equal(expectedDescription, actualDescription);
            Assert.Equal(expectedVideoUrl, actualVideoUrl);
            Assert.Equal(expectedUserId, actualUserId);
            Assert.Equal(expectedCategoryId, actualCategoryId);
        }

        [Fact]
        public async Task CreateAsyncWithCourseShuldWorkCorrect()
        {
            // Arrange
            this.lesson.CourseId = "Test";

            // Act
            await this.lessonsService.CreateAsync(this.lesson.Title, this.lesson.Description, this.lesson.VideoUrl, this.lesson.UserId, this.lesson.CategoryId, this.lesson.CourseId);

            var lesson = await this.lessonsRepository.All().FirstOrDefaultAsync();

            var expectedTitle = this.lesson.Title;
            var actualTitle = lesson.Title;

            var expectedDescription = this.lesson.Description;
            var actualDescription = lesson.Description;

            var expectedVideoUrl = this.lesson.VideoUrl;
            var actualVideoUrl = lesson.VideoUrl;

            var expectedUserId = this.lesson.UserId;
            var actualUserId = lesson.UserId;

            var expectedCategoryId = this.lesson.CategoryId;
            var actualCategoryId = lesson.CategoryId;

            var expectedCourseId = this.lesson.CourseId;
            var actualCourseId = lesson.CourseId;

            // Assert
            Assert.Equal(expectedTitle, actualTitle);
            Assert.Equal(expectedDescription, actualDescription);
            Assert.Equal(expectedVideoUrl, actualVideoUrl);
            Assert.Equal(expectedUserId, actualUserId);
            Assert.Equal(expectedCategoryId, actualCategoryId);
            Assert.Equal(expectedCourseId, actualCourseId);
        }

        [Fact]
        public async Task ByIdAsyncShouldReturnCorrectValue()
        {
            // Arrange

            // Act
            await this.FillData(1);

            var result = await this.lessonsRepository.AllAsNoTracking().FirstOrDefaultAsync();

            var lesson = await this.lessonsService.ByIdAsync<LessonModel>(result.Id);

            // Assert
            Assert.Equal(this.lesson.Title, lesson.Title);
            Assert.Equal(this.lesson.Description, lesson.Description);
            Assert.Equal(this.lesson.VideoUrl, lesson.VideoUrl);
            Assert.Equal(this.lesson.UserId, lesson.UserId);
            Assert.Equal(this.lesson.CategoryId, lesson.CategoryId);
        }

        [Fact]
        public async Task EditAsyncShouldWorkCorrect()
        {
            // Arrange

            // Act
            await this.FillData(1);

            var lesson = await this.lessonsRepository.All().FirstOrDefaultAsync();

            var expected = "Changed title";

            await this.lessonsService.EditAsync(lesson.Id, expected, lesson.Description, lesson.VideoUrl, lesson.CategoryId);

            var actual = lesson.Title;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetByCategoryIdAsyncShouldReturnCorrectValues()
        {
            // Arrange

            // Act
            await this.FillData(2);

            await this.lessonsService.CreateAsync(this.lesson.Title, this.lesson.Description, this.lesson.VideoUrl, this.lesson.UserId, 2);

            var lessons = await this.lessonsService.GetByCategoryIdAsync<LessonModel>(this.lesson.CategoryId, 1, 10);

            var expectedCount = 2;
            var actualCount = lessons.Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task GetByCategoryIdAsyncShouldReturnDataInCorrectOrder()
        {
            // Arrange
            var expextedTitle = "Latest";

            // Act
            await this.FillData(2);

            await this.lessonsService.CreateAsync(expextedTitle, this.lesson.Description, this.lesson.VideoUrl, this.lesson.UserId, this.lesson.CategoryId);
            await this.lessonsService.CreateAsync(this.lesson.Title, this.lesson.Description, this.lesson.VideoUrl, this.lesson.UserId, 2);

            var lessons = await this.lessonsService.GetByCategoryIdAsync<LessonModel>(this.lesson.CategoryId, 1, 10);

            var actualTitle = lessons.FirstOrDefault().Title;

            // Assert
            Assert.Equal(expextedTitle, actualTitle);
        }

        [Fact]
        public async Task GetByUserIdAsyncShouldReturnCorrectValues()
        {
            // Arrange
            var expectedCount = 2;

            // Act
            await this.FillData(expectedCount);

            await this.lessonsService.CreateAsync(this.lesson.Title, this.lesson.Description, this.lesson.VideoUrl, "userId", 2);

            var lessons = await this.lessonsService.GetByUserIdAsync<LessonModel>(this.lesson.UserId);

            var actualCount = lessons.Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task GetByUserIdAsyncShouldReturnDataInCorrectOrder()
        {
            // Arrange
            var expectedTitle = "Last";

            // Act
            await this.FillData(2);

            await this.lessonsService.CreateAsync(expectedTitle, this.lesson.Description, this.lesson.VideoUrl, this.lesson.UserId, this.lesson.CategoryId);

            var lessons = await this.lessonsService.GetByUserIdAsync<LessonModel>(this.lesson.UserId);

            var actualTitle = lessons.FirstOrDefault().Title;

            // Assert
            Assert.Equal(expectedTitle, actualTitle);
        }

        [Fact]
        public async Task DeleteAsyncShuldWorkCorrect()
        {
            // Arrange

            // Act
            await this.FillData(2);

            var lessonid = this.lessonsRepository.All().FirstOrDefault().Id;

            await this.lessonsService.DeleteAsync(lessonid);

            var expectedCount = 1;
            var actualCount = this.lessonsRepository.All().Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task GetCountByCategoryShouldReturnCorrectValue()
        {
            // Arrange
            var expectedCount = 2;

            // Act
            await this.FillData(expectedCount);

            var actualCount = this.lessonsService.GetCountByCategory(this.lesson.CategoryId);

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
            this.lessonsRepository.Dispose();
        }

        private async Task FillData(int count)
        {
            for (int i = 0; i < count; i++)
            {
                await this.lessonsService.CreateAsync(this.lesson.Title, this.lesson.Description, this.lesson.VideoUrl, this.lesson.UserId, this.lesson.CategoryId);
            }
        }
    }
}
