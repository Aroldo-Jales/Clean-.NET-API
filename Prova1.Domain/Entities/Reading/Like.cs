using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prova1.Domain.Entities.Reading
{
    public class Like
    {
        [Key]        
        public int Id { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("Reading")]
        public int ReadingId { get; set; }

        public Like(Guid userId, int readingId)
        {            
            UserId = userId;
            ReadingId = readingId;
        }
    }
}
