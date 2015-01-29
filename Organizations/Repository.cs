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

    public class Repository<T> : IRepository<T>
    {
        protected List<T> data;

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

        public T GetEntity(int index = 0)
        {
            return data[index];
        } 

        public List<T> GetAll()
        {
            return data;
        }

    }


}
