using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbEntity
{
    class DepartmentDb : IEntityDb
    {
        public int OrganizationId { get; private set; }
        public int Id { get; private set; }
        public string Name { get; set; }
    }
}
