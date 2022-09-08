using System.ComponentModel.DataAnnotations;

namespace Prova1.Domain.Entities.Authentication
{
    public class User
    {
        // Usuário (id, name, login, telefone, password, conta ativa)
        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(300)]
        public string Name { get; set; } = null!;

        [MaxLength(50)]
        public string Email { get; set; } = null!;

        [MaxLength(11)]
        public string? PhoneNumber { get; set; }

        public string PasswordHash { get; set; } = null!;

        public byte[] Salt { get; set; } = null!;

        public bool ActiveAccount { get; set; } = false;

        public ICollection<RefreshToken>? RefreshTokens { get; set; }

        public int DeviceLimit { get; } = 4;

        public ICollection<UserValidationCode>? UserValidationCodes { get; set; }
    }
}