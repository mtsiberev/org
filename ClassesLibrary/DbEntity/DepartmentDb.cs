
namespace Organizations.DbEntity
{
    public class DepartmentDb : IEntityDb
    {
        private readonly int m_id;
        private readonly int m_organizationId;
        public string Name { get; set; }

        public DepartmentDb(int id, int parentOrganization)
        {
            m_id = id;
            m_organizationId = parentOrganization;
        }
        
        public int Id
        {
            get
            {
                return m_id;
            }
        }

        public int ParentOrganizationId
        {
            get
            {
                return m_organizationId;
            }
        }
    }
}
