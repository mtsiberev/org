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
        private static volatile RegisterByContainer s_instance;
        private static object syncRoot = new Object();

        public IContainer Container;
        private RegisterByContainer()
        {
            Container = new Container(x =>
            {
                x.For<IRepository<Organization>>().Singleton().Use<Repository<Organization>>();
                x.For<IRepository<Department>>().Singleton().Use<Repository<Department>>();
                x.For<IRepository<Employee>>().Singleton().Use<Repository<Employee>>();
                x.For<Facade>().Singleton().Use<Facade>();
                x.For<Reports>().Singleton().Use<Reports>();
            });
        }

        public static RegisterByContainer Instance
        {
            get
            {
                if (s_instance != null) return s_instance;
                lock (syncRoot)
                {
                    if (s_instance == null)
                        s_instance = new RegisterByContainer();
                }
                return s_instance;
            }
        }
    }

}
