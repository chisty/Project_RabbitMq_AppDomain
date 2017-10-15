using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Topshelf;

namespace ArchiDesignHandsOn1
{
    class Program
    {

        /// <summary>
        /// Architecture with RabbitMq and separate Appdomain. Used MEF and TopShelf with that.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Main. Current AppDomain= "+ AppDomain.CurrentDomain.FriendlyName);                      

            Thread.CurrentThread.Name = "Main EventHandler Thread";
            HostFactory.Run(x =>
            {
                x.Service<HandlerService>();
                x.RunAsLocalSystem();
                x.SetDescription("Test Description: Chisty");
                x.SetDisplayName("Test Display: Chisty");
                x.SetServiceName("Test Service: Chisty");
                x.StartAutomatically();
            });
        }
    }
}
