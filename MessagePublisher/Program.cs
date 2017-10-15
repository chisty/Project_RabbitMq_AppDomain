using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagePublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var publisher = new Publisher();
            publisher.CreateConnection();
            publisher.SendMessages();
        }
    }
}
