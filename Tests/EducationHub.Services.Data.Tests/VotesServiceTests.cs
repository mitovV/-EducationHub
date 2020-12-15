namespace EducationHub.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EducationHub.Data;
    using EducationHub.Data.Common.Repositories;
    using EducationHub.Data.Models;
    using EducationHub.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Votes;
    using Xunit;

    public class VotesServiceTests
    {
        private readonly EfRepository<Vote> votesRepository;
        private readonly VotesService votesService;

        public VotesServiceTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<EducationHubDbContext>()
               .UseInMemoryDatabase("db");

            var dbCntext = new EducationHubDbContext(optionsBuilder.Options);

            this.votesRepository = new EfRepository<Vote>(dbCntext);

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
    }
}
