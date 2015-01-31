using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsNS
{
    /*
    public interface IRepository<T>
    {
        void Insert(T entity);
        void Delete(T entity);
        List<T> GetAll();
    }
    */
    // public class Repository<T> : IRepository<T>

    public class Repository
    {
        protected List<Organization> data;

        public Repository() 
        { 
            data = new List<Organization>() ;
        }


        public int GetNewEntityId()
        {
            return this.data.Count;
        }

        public void Insert(Organization entity)
        {
            data.Add(entity);
        }

        public void Delete(Organization entity)
        {

        }

        public Organization GetEntityById(int id)
        {
            return data.Find(x => x.Id == id);
        }

        public List<Organization> GetAllOrganizations()
        {
            return data;
        }

    }


}
