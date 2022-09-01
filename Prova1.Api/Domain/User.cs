namespace Prova1.Api.Domain
{
    public class User
    {
        public User(string name, string email, string telefone, string password)
        {            
            Name = name;
            Email = email;
            Telefone = telefone;
            Password = password;
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public string Password { get; set; } = null!;   
        public bool ContaAtiva { get; set; } = false;
    }
}