using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Interfaces;
using Contracts.Models;

namespace EventHandler
{
    public abstract class AHandler: IHandler
    {
        public object Process(Message payload)
        {
            Console.WriteLine("Inside AHandler. Message Type= "+payload.Type);
            return DoProcess(payload);
        }

        public abstract object DoProcess(Message payload);        

        public abstract string GetHandlerType();
    }
}
