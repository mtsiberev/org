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
        private List<T> data;

        public void Insert(T entity) { data.Add(entity); }
        public void Delete(T entity) { }
        public List<T> GetAll() { return data; }

        public Repository() { data = new List<T>(); }

        public int GetNewEntityId() { return this.data.Count; }
        public T GetEntityById(int id) { return data.Find(x => x.Id == id); }

    }
}
