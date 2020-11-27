namespace EducationHub.Data.Models
{
    using Common.Models;

    public class Vote : BaseModel<int>
    {
        public string VotedId { get; set; }

        public virtual User Voted { get; set; }

        public string VotedForId { get; set; }

        public virtual User VotedFor { get; set; }

        public byte Value { get; set; }
    }
}
