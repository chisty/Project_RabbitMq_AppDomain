using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using Contracts.Interfaces;
using Contracts.Models;
using EventHandler;

namespace ArchiDesignHandsOn1
{    
    public class WorkerAppDomain: MarshalByRefObject, IWorker
    {
        public void Test()
        {
            Console.WriteLine("Hello from WorkerAppDomain. Domain Name= {0}", AppDomain.CurrentDomain.FriendlyName);
            IocContainer.Instance.AddAssembly<IHandler>();
            IocContainer.Instance.AddAssembly<PurchaseHandler>();
            var executingAssembly = Assembly.GetExecutingAssembly();
            IocContainer.Instance.AddAssembly(executingAssembly);            
        }

        public object DoWork(Message msg)
        {
            var taskHandler = IocContainer.Instance.Resolve<TaskHandler>();
            return taskHandler.DoWork(msg);
            
        }


        

    }
}
