using System;
using StructureMap;
using OrganizationsWebApplication.Helpers;

namespace OrganizationsWebApplication.IoC
{
    public sealed class MvcContainer
    {
        private static object syncRoot = new Object();
        private static volatile IContainer s_container;
        private MvcContainer() { }

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
                            x.For<ImageObject>().Use<ImageObject>()
                                .Ctor<int>("id");

                            x.For<WcfService.Service>().Use<WcfService.Service>();
                        });
                }
                return s_container;
            }
        }
    }
}