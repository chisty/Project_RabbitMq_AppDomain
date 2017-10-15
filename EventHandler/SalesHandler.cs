using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Contracts.Interfaces;
using Contracts.Models;

namespace EventHandler
{
    [Export(typeof(IHandler))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SalesHandler: AHandler
    {
        public override object DoProcess(Message payload)
        {
            Console.WriteLine("Inside SalesHandler Handler:: payload= "+payload.Text);
            return null;
        }

        public override string GetHandlerType()
        {
            return Constants.Types.SalesType;
        }
    }
}
