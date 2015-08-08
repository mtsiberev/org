
namespace Organizations
{
    public interface IEntity
    {
        int Id { get; }     
        int GetEntityCode();

        string Name { get; set; }
    }
}
