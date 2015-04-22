using System;
using System.Collections.Generic;

namespace Organizations.DbEntity
{
    public class RepositoryDb<T> : IRepository<T> where T : class, IEntityDb
    {
        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            return AdoHelper.GetEntity(typeof(T), id);
        }

        public T GetRandom()
        {
            throw new NotImplementedException();
        }
    }
}
