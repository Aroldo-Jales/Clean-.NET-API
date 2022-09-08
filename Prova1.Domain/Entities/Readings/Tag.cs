
namespace Prova1.Domain.Entities.Readings
{
    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }
     
        public string Description { get; set; }

        public Tag(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
