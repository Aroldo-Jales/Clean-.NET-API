using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Prova1.Domain.Entities.Readings
{
    public class Commentary
    {
        public Commentary()
        {
        }

        public Commentary(Guid userId, Guid readingId, string content)
        {
            UserId = userId;
            ReadingId = readingId;
            Content = content;
        }

        [Key]
        public Guid Id = Guid.NewGuid();

        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required]
        [ForeignKey("Reading")]
        public Guid ReadingId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; } = null!;
    }
}