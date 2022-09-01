namespace Prova1.Api.Domain
{
    public class UserValidationCode
    {
        public UserValidationCode(int userId, int code, DateTime expiration)
        {            
            UserId = userId;
            Code = code;
            Expiration = expiration;
        }

        public int UserId { get; set; }
        public int Code { get; set; }
        public DateTime Expiration { get; set; }
    }
}