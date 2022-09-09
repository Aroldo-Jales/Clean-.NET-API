using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prova1.Domain.Entities.Reading
{
    public class Reading
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(70)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(70)]
        public string SubTitle { get; set; } = null!;

        [Required]
        [ForeignKey("Tag")]
        public int TagId { get; set; }

        [Required]
        public bool Stopped { get; set; } = false;

        [Required]
        public bool Completed { get; set; } = false;

        [Required]
        public int CurrentPage { get; set; } = 1;
    }
}
