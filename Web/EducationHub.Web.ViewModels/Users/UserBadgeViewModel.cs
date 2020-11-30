namespace EducationHub.Web.ViewModels.Users
{
    using System.Globalization;

    public class UserBadgeViewModel
    {
        public string Id { get; set; }

        public int Votes { get; set; }

        public string PictureUrl { get; set; }

        public string Username { get; set; }

        public double AverageVote { get; set; }

        public string AvarageVoteAsString => this.AverageVote.ToString("0.0", CultureInfo.InvariantCulture);
    }
}
