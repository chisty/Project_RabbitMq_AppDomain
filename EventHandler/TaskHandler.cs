using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Interfaces;
using Contracts.Models;

namespace EventHandler
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TaskHandler
    {
        public object DoWork(Message msg)
        {
            if (Handlers != null && Handlers.Any())
            {
                var handler = Handlers.FirstOrDefault(h => h.GetHandlerType() == msg.Type);
                return handler?.Process(msg);
            }
            return null;
        }

        #region Property

        [ImportMany]
        public List<IHandler> Handlers { get; set; }

        #endregion
    }
}
