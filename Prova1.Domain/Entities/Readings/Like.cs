using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prova1.Domain.Entities.Readings
{
    public class Like
    {
        public Like()
        {
        }

        public Like(Guid userId, Guid readingId)
        {
            UserId = userId;
            ReadingId = readingId;
        }

        [Key]        
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("Reading")]
        public Guid ReadingId { get; set; }
    }
}
