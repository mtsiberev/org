using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;

namespace Organizations
{
    public sealed class RegisterByContainer
    {
        private static object syncRoot = new Object();
        private static volatile IContainer s_container;
        
        public static IContainer Container
        {
            get
            {
                if (s_container != null) return s_container;
                lock (syncRoot)
                {
                    if (s_container == null)
                        s_container = new Container(x =>
                        {
                            x.For<IRepository<Organization>>().Singleton().Use<Repository<Organization>>();
                            x.For<IRepository<Department>>().Singleton().Use<Repository<Department>>();
                            x.For<IRepository<Employee>>().Singleton().Use<Repository<Employee>>();
                            x.For<Facade>().Singleton().Use<Facade>();
                            x.For<Reports>().Singleton().Use<Reports>();
                        });
                }
                return s_container;
            }
        }
    }

}
