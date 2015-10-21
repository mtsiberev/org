using System;
using Organizations.DbRepository;
using StructureMap;

namespace Organizations
{
    public sealed class RegisterByContainer
    {
        private static object syncRoot = new Object();
        private static volatile IContainer s_container;
        private RegisterByContainer() { }

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
                            x.For<IRepository<Organization>>().Singleton().Use<RepositoryOrganizationDatabase>();
                            x.For<IRepository<Department>>().Singleton().Use<RepositoryDepartmentDatabase>();
                            x.For<IRepository<Employee>>().Singleton().Use<RepositoryEmployeeDatabase>();
                            
                            x.For<Facade>().Singleton().Use<Facade>()
                                .Ctor<IRepository<Organization>>()
                                .Is(new RepositoryOrganizationDatabase())
                                .Ctor<IRepository<Department>>()
                                .Is(new RepositoryDepartmentDatabase())
                                .Ctor<IRepository<Employee>>()
                                .Is(new RepositoryEmployeeDatabase());
                            
                            x.For<Reports>().Singleton().Use<Reports>();
                        });
                }
                return s_container;
            }
        }
    }

}
