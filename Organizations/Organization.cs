using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations
{
    public class Organization : IEntity
    {
        private int id;
        public Organization() { }

        public int Id
        {
            get
            {
                return id;
            }
        }
        public string Name { get; set; }
    }
}
