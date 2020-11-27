namespace EducationHub.Services.Data.Votes
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        Task SetVoteAsync(string votedId, string votedForId, byte value);

        double GetAverageVotes(string userId);
    }
}
