using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string Name { get; set; }
        public new int GetEntityCode()
        {
            return 0;
        }
    }
}
