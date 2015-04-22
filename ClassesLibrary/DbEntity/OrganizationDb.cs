using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbEntity
{
    public class OrganizationDb : IEntityDb
    {
        public int Id { get; private set; }
        public string Name { get; set; }
    }
}
