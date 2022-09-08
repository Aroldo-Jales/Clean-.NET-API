using System.ComponentModel.DataAnnotations;

namespace Prova1.Domain.Entities.Readings
{
    public class Reading
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid userId { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string SubTitle { get; set; } = null!;

        [Required]
        public int TagId { get; set; }

       

    }
}
