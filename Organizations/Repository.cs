using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsNS
{
    public interface IRepository<T>
    {
        void Insert(T entity);
        void Delete(T entity);
        List<T> GetAll();
    }

    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected List<T> data;        

        public Repository()
        {
            data = new List<T>();
        }

        public int GetNewEntityId()
        {
            return this.data.Count;
        }

        public void Insert(T entity)
        {
            data.Add(entity);
        }

        public void Delete(T entity)
        {

        }

        public T GetEntityById(int id)
        {
            return data.Find(x => x.Id == id);
        }

        public List<T> GetAll()
        {
            return data;
        }

    }


}
