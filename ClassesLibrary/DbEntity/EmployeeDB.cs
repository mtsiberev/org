using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbEntity
{
    class EmployeeDb : IEntityDb
    {
        public int Id { get; private set; }
    }
}
