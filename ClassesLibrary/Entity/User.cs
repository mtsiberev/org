
namespace Organizations.Entity
{
    public class User
    {
        public User(int id, string name, string role)
        {
            Id = id;
            Name = name;
            Role = role;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Role { get; private set; }
    }
}
