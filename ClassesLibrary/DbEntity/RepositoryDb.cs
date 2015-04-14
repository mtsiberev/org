using System;
using System.Collections.Generic;

namespace Organizations.DbEntity
{
    class RepositoryDb<T> : IRepository<T> where T : class, IEntityDb
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
            throw new NotImplementedException();
        }

        public T GetRandom()
        {
            throw new NotImplementedException();
        }
    }
}
