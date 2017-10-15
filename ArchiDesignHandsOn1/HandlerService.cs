using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace ArchiDesignHandsOn1
{
    public class HandlerService : ServiceControl
    {
        public bool Start(HostControl hostControl)
        {
            Console.WriteLine("Inside Handler Service.");
            MessageHandler = IocContainer.Instance.Resolve<MessageHandler>();
            MessageHandler.CreateAppDomains();
            MessageHandler.Process();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {            
            MessageHandler?.Dispose();
            return true;
        }

        #region Property
        
        public MessageHandler MessageHandler;

        #endregion
    }
}
