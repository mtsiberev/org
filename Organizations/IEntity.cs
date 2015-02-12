using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations 
{
    public interface IEntity
    {
        Guid Id { get; }
        Guid ParentId { get; }
        string Name { get; set; }
    }
}
