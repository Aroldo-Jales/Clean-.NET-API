using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Prova1.Domain.Entities.Reading
{
    public class Annotation
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Reading")]
        public int ReadingId { get; set; }

        [Required]
        public string Content { get; set; } = null!;

        [Required]
        public int Page { get; set; }

        public Annotation(int readingId, string content, int page)
        {            
            ReadingId = readingId;
            Content = content;
            Page = page;
        }
    }
}