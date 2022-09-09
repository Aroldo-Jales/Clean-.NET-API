using System.ComponentModel.DataAnnotations;

namespace Prova1.Domain.Entities.Reading
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool stopped { get; set; } = false;

        [Required]
        public bool completed { get; set; } = true;

        public Tag(string name, string description)
        {            
            Name = name;
            Description = description;
        }
    }
}
