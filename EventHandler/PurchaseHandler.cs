using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
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
    public class PurchaseHandler: AHandler
    {
        public override object DoProcess(Message payload)
        {
            Console.WriteLine("Inside PurchaseHandler Handler:: payload= " + payload.Text);
            return null;
        }

        public override string GetHandlerType()
        {
            return Constants.Types.PurchaseType;
        }
    }
}
