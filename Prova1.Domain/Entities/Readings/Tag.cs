using System.ComponentModel.DataAnnotations;

namespace Prova1.Domain.Entities.Readings
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
