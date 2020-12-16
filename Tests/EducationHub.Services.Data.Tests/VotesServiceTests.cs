namespace EducationHub.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EducationHub.Data;
    using EducationHub.Data.Models;
    using EducationHub.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Votes;
    using Xunit;

    public class VotesServiceTests : IDisposable
    {
        private readonly EfRepository<Vote> votesRepository;
        private readonly VotesService votesService;

        public VotesServiceTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<EducationHubDbContext>()
               .UseInMemoryDatabase("db");

            var dbContext = new EducationHubDbContext(optionsBuilder.Options);

            this.votesRepository = new EfRepository<Vote>(dbContext);

            this.votesService = new VotesService(this.votesRepository);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task SetVoteShouldWorkCorrect(byte voteValue)
        {
            // Arrange

            // Act
            var isSuccsesVote = await this.votesService.SetVoteAsync("1", "2", voteValue);

            var expectedVoteValue = voteValue;
            var actualVoteValue = this.votesRepository.All().FirstOrDefault().Value;

            // Assert
            Assert.True(isSuccsesVote);
            Assert.Equal(expectedVoteValue, actualVoteValue);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task SetVoteShouldSetOnlyOneVoteWhenUserVotesMoreThenOneTime(byte voteValue)
        {
            // Arrange

            // Act
            await this.votesService.SetVoteAsync("1", "2", voteValue);

            var expectedVotesCount = 1;
            var actualVotesVCount = this.votesRepository.All().Count();

            // Assert
            Assert.Equal(expectedVotesCount, actualVotesVCount);
        }

        [Fact]
        public async Task SetVoteShouldNotSetValueWhenUserVotesForHimself()
        {
            // Arrange

            // Act
            var isSuccsesVote = await this.votesService.SetVoteAsync("1", "1", 3);

            // Assert
            Assert.False(isSuccsesVote);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool value)
        {
            this.votesRepository.Dispose();
        }
    }
}
