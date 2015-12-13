using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace XmlServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost serviceHost = null;
            try
            {
                serviceHost = new ServiceHost(typeof(XmlServiceLibrary.XmlService));
                
                ServiceDebugBehavior debug = serviceHost.Description.Behaviors.Find<ServiceDebugBehavior>();

               if (debug == null)
                {
                    serviceHost.Description.Behaviors.Add(
                         new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
                }
                else
                {
                    if (!debug.IncludeExceptionDetailInFaults)
                    {
                        debug.IncludeExceptionDetailInFaults = true;
                    }
                }
                

                serviceHost.Open();
                Console.WriteLine("\n");
                Console.WriteLine("Xml service is running at following address");
                Console.WriteLine("\n");
                Console.WriteLine("http://localhost:80/XmlService");
                Console.WriteLine("\n");
                Console.WriteLine("net.tcp://localhost:808/XmlService");
            }
            catch (Exception ex)
            {
                serviceHost = null;
                Console.WriteLine("Service can not be started \n\nError Message [" + ex.Message + "]");
            }
            if (serviceHost != null)
            {
                Console.WriteLine("\nPress any key to close the Service");
                Console.ReadKey();
                serviceHost.Close();
            } 
        }
    }
}
