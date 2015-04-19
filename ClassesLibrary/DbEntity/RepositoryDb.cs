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
            var table = AdoHelper.GetEmployeeById(id);
            var reader = table.CreateDataReader();
            int depId;
            string name;

            while (reader.Read())
            {
                id = (int)reader.GetValue(0);
                depId = (int)reader.GetValue(1);
                name = reader.GetValue(2).ToString();
            }
            //return new (id, depId) { Name = name };
            return null;
        }

        public T GetRandom()
        {
            throw new NotImplementedException();
        }
    }
}
