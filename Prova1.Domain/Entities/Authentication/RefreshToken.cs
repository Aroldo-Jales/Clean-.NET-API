using System.ComponentModel.DataAnnotations;

namespace Prova1.Domain.Entities.Authentication
{
    public class RefreshToken
    {
        [Key]        
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public string Token { get; set; } = null!;

        public DateTime Created { get; set; }

        public DateTime Expires { get; set; }

        public string Device { get; set; } = null!;

        public string Iat { get; set; } = null!;

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