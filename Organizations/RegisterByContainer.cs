using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;

namespace Organizations
{
    public class RegisterByContainer
    {
        public IContainer Container;

        public RegisterByContainer()
        {
            Container = new Container(x =>
            {
                x.For<IRepository<Organization>>().Singleton().Use<Repository<Organization>>();
                x.For<IRepository<Department>>().Singleton().Use<Repository<Department>>();
                x.For<IRepository<Employee>>().Singleton().Use<Repository<Employee>>();

                x.For<Reports>().Singleton().Use<Reports>();

                x.For<Reports>().Singleton().Use<Reports>()
                    .Ctor<Facade>().Is<Facade>();
                
                x.For<Facade>().Singleton().Use<Facade>()
                    .Ctor<Repository<Organization>>().Is<Repository<Organization>>()
                    .Ctor<Repository<Department>>().Is<Repository<Department>>()
                    .Ctor<Repository<Employee>>().Is<Repository<Employee>>();
            });
        }


    }
}
