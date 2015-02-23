using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations
{
    public class Organization : IEntity
    {
        private int _id;
        public Organization(int id)
        {  
            _id = id;
        }

        public int Id
        {
            get
            {
                return _id;
            }
        } 

        public string Name { get; set; }
        public new int GetEntityCode()
        {
            return 0;
        }
    }
}
