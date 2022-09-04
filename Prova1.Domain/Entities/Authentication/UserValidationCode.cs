namespace Prova1.Domain.Entities.Authentication
{
    public class UserValidationCode
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Type { get; set; }
        public int Code { get; set; }       
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
            get {
                Random RandNum = new Random();
                int number = RandNum.Next(100000, 999999);

                Console.WriteLine("Codigo de confirmacao para: "+Type+" = "+number);

                return number;
            }            
        }

        private DateTime GetExpirationDate
        {
            get {
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