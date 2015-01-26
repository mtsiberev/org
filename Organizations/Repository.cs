using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations
{
    public interface IRepository<T>
    {
        void Insert(T entity);
        void Delete(T entity);
        List<T> GetAll();

    }


    public class InstancesRepository<T> : IRepository<T>
    {
        protected List<T> data;

        public void Insert(T entity)
        {
            data.Add(entity);
        }

        public void Delete(T entity)
        {

        }

        public List<T> GetAll()
        {
            return data;
        }

    }


}
