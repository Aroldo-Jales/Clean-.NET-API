using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prova1.Domain.Entities.Authentication
{
    public class UserValidationCode
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required]
        public string Type { get; set; } 

        [Required]
        public int Code { get; set; }

        [Required]
        public DateTime Expiration { get; set; }

        public UserValidationCode(Guid userId, string type)
        {
            UserId = userId;
            Type = type;
            Code = GetRandomNumber;
            Expiration = GetExpirationDate;
        }

        private int GetRandomNumber
        {
            get
            {
                Random RandNum = new Random();
                int number = RandNum.Next(100000, 999999);


                Console.WriteLine("Codigo de confirmacao para: " + Type + " = " + number);

                return number;
            }
        }

        private DateTime GetExpirationDate
        {
            get
            {
                return DateTime.Now.AddHours(2);
            }
        }

        public void RenewUserValidationCode()
        {
            Code = GetRandomNumber;
            Expiration = GetExpirationDate;
        }
    }
}