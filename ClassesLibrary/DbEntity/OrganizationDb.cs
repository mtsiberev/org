using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbEntity
{
    public class OrganizationDb : IEntityDb
    {
        private readonly int m_id;
        public string Name { get; set; }

        public OrganizationDb(int id)
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
    }
}
