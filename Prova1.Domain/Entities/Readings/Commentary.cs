using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Prova1.Domain.Entities.Readings
{
    public class Commentary
    {
        [Key]
        public int Id;

        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required]
        [ForeignKey("Reading")]
        public int ReadingId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; } = null!;

        public Commentary(Guid userId, string content)
        {
            UserId = userId;
            Content = content;
        }
    }
}