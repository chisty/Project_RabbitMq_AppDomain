using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Models;

namespace Contracts.Interfaces
{
    public interface IHandler
    {
        object Process(Message payload);
        string GetHandlerType();
    }
}
