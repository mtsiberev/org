
namespace Organizations
{
    public class Employee : IEntity
    {
        private readonly int m_id;
        public Employee(int id, Department parentDepartment)
        {
            m_id = id;
            ParentDepartment = parentDepartment;
        }
        
        public int Id
        {
            get
            {
                return m_id;
            }
        }

        public Department ParentDepartment { get; set; }
    
        public string LastName { get; set; }
        public int Age { get; set; }
        public Address Address { get; set; }

        public new int GetEntityCode()
        {
            return 2;
        }

        public string Name { get; set; }
    }
}
