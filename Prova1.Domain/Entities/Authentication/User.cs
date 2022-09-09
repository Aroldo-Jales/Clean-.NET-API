using System.ComponentModel.DataAnnotations;

namespace Prova1.Domain.Entities.Authentication
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(300)]
        public string Name { get; set; } = null!;

        [MaxLength(50)]
        public string Email { get; set; } = null!;

        [MaxLength(11)]
        public string? PhoneNumber { get; set; }

        [Required]
        public string PasswordHash { get; set; } = null!;

        public byte[] Salt { get; set; } = null!;

        [Required]
        public bool ActiveAccount { get; set; } = false;

        [Required]
        public int DeviceLimit { get; } = 4;

        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }

    }
}