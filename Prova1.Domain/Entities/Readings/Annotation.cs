using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Prova1.Domain.Entities.Readings
{
    public class Annotation
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Reading")]
        public Guid ReadingId { get; set; }

        [Required]
        public string Content { get; set; } = null!;

        [Required]
        public int Page { get; set; }
    }
}