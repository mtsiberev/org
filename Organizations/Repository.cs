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
        private readonly List<T> m_data;

        public Repository()
        {
            m_data = new List<T>();
        }

        public void Insert(T entity)
        {
            m_data.Add(entity);
        }

        public void Delete(T entity) { }

        public IEnumerable<T> GetAll()
        {
            return m_data;
        }

        public T GetById(int id)
        {
            return m_data.Single(e => e.Id.Equals( id ) );
        }        
    }
}
