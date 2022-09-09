using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prova1.Domain.Entities.Authentication
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]       
        public Guid UserId { get; set; }

        [Required]
        public string Token { get; set; } = null!;

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime Expires { get; set; }

        [Required]
        public string Device { get; set; } = null!;
        
        [Required]
        public string? Iat { get; set; }

        public RefreshToken(Guid userId, string token, DateTime created, DateTime expires, string device, string iat)
        {
            UserId = userId;
            Token = token;
            Created = created;
            Expires = expires;
            Device = device;
            Iat = iat;
        }
    }
}