using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Organizations
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private List<T> data;

        public Repository()
        {
            data = new List<T>();
        }

        public void Insert(T entity)
        {
            data.Add(entity);
        }

        public void Delete(T entity) { }

        public IEnumerable<T> GetAll()
        {
            return data;
        }

        public T GetById(Guid id)
        {
            return data.Single(e => e.Id.Equals( id ) );
        }

        public T GetByName(string name)
        {
            return data.Single(e => e.Name.Equals(name) );
        }
  
        
    }
}
