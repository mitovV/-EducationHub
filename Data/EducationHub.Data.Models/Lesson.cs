namespace EducationHub.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Common.Models;

    using static Common.Validations.DataValidation.Lesson;

    public class Lesson : BaseDeletableModel<string>
    {
        public Lesson()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(VideoUrlMaxLength)]
        public string VideoUrl { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}
