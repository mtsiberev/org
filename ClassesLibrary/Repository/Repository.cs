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
        static Random random = new Random((DateTime.Now.Millisecond));

        public Repository()
        {
            m_data = new List<T>();
        }

        public void Insert(T entity)
        {
            m_data.Add(entity);
        }

        public void Delete(int id) { }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll()
        {
            return m_data;
        }

        public T GetById(int id)
        {
            return m_data.First(e => e.Id.Equals(id));
        }

        public T GetRandom()
        {
            var randomNext = random.Next(0, m_data.Count);
            return m_data[randomNext];
        }

        public List<Organization> GetEntitiesForOnePage(int pageNum, int pageSize, string sortType)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }
    }
}
