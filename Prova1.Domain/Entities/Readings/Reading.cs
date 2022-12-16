using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prova1.Domain.Entities.Readings
{
    public class Reading
    {
        public Reading()
        {
        }

        public Reading(Guid userId, string title, string subTitle, bool stopped, bool completed, int currentPage)
        {            
            UserId = userId;
            Title = title;
            SubTitle = subTitle;            
            Stopped = stopped;
            Completed = completed;
            CurrentPage = currentPage;
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(70)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(70)]
        public string SubTitle { get; set; } = null!;

        [Required]
        [ForeignKey("Tag")]
        public int TagId { get; set; } = 1;

        [Required]
        public bool Stopped { get; set; } = false;

        [Required]
        public bool Completed { get; set; } = false;

        [Required]
        public int CurrentPage { get; set; } = 1;
    }
}
