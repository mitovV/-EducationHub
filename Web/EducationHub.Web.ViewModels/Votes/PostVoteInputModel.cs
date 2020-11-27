namespace EducationHub.Web.ViewModels.Votes
{
    using System.ComponentModel.DataAnnotations;

    using static Data.Common.Validations.DataValidation.Vote;

    public class PostVoteInputModel
    {
        [Required]
        public string VotedForId { get; set; }

        [Range(MinValue, MaxValue)]
        public byte Value { get; set; }
    }
}
