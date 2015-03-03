using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations
{
    public class MockRepository<T> : IRepository<T> where T : class, IEntity
    {
        private List<T> data;
        public T LastInsertedEntity { get; private set; }
        public T LastDeletedEntity { get; private set; }


        public MockRepository()
        {
            data = new List<T>();
        }        

        public void Insert(T entity)
        {
            data.Add(entity);
            LastInsertedEntity = entity;
        }     

        public void Delete(T entity) { }

        public IEnumerable<T> GetAll()
        {
            return data;
        }

        public T GetById(int id)
        {
            return data.Single(e => e.Id.Equals( id ) );
        }    
    }
}
