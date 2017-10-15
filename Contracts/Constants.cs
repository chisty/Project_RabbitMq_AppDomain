using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class Constants
    {
        public class Types
        {
            public const string NotificationType = "NotificationType";
            public const string PurchaseType = "PurchaseType";
            public const string SalesType = "SalesType";
        }

        public class Messages
        {
            public const string ExchangeName = "ChistyExchange";
            public const string QueueName = "Queue1";
            public const string RoutingKey = "hello";
            public const string HostName = "localhost";
            public const string UserName = "guest";


        }
    }
}
