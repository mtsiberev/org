
namespace Organizations
{
    public class Organization : IEntity
    {
        private readonly int m_id;
        public Organization(int id)
        {  
            m_id = id;
        }

        public int Id
        {
            get
            {
                return m_id;
            }
        } 

        public new int GetEntityCode()
        {
            return 0;
        }

        public string Name { get; set; }
    }
}
