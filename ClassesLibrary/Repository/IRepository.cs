using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Organizations
{
    public interface IRepository<T> : IPaging<Organization>
    {
        void Insert(T entity);
        void Delete(int id);
        void Update(T entity);
        List<T> GetAll();
        T GetById(int id);
        T GetRandom();
    }
}
