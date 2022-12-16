using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Prova1.Domain.Entities.Readings
{
    public class Annotation
    {
        public Annotation(Guid readingId, string content, int page)
        {
            ReadingId = readingId;
            Content = content;
            Page = page;
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("Reading")]
        public Guid ReadingId { get; set; }

        [Required]
        public string Content { get; set; } = null!;

        [Required]
        public int Page { get; set; }
    }
}