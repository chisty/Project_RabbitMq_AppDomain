using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Contracts.Interfaces;
using Contracts.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;

namespace ArchiDesignHandsOn1
{
    [Export(typeof(MessageHandler))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MessageHandler: IDisposable
    {        
        public MessageHandler()
        {
            WorkerItems= new List<WorkerItem>();
            _factory = new ConnectionFactory
            {
                HostName = Constants.Messages.HostName,
                UserName = Constants.Messages.UserName,
                Password = Constants.Messages.UserName
            };
            count = 0;
        }

        public void Process()
        {
            using (_connection = _factory.CreateConnection())
            {
                using (var channel = _connection.CreateModel())
                {                    
                    channel.ExchangeDeclare(Constants.Messages.ExchangeName, "direct");
                    
                    channel.QueueDeclare(Constants.Messages.QueueName, true, false, false, null);
                    channel.QueueBind(Constants.Messages.QueueName, Constants.Messages.ExchangeName, Constants.Messages.RoutingKey);
                    channel.BasicQos(0, 1, false);
                    var conumer= new QueueingBasicConsumer(channel);
                    channel.BasicConsume(Constants.Messages.QueueName, false, conumer);                    
                    while (true)
                    {
                        var deliveryArguments = conumer.Queue.Dequeue();
                        var message = deliveryArguments.Body.DeSerialize(typeof(Message));
                        var msg = message as Message;
                        if (msg != null)
                        {
                            var index = count%3;
                            var workerItem = WorkerItems[index];
                            Console.WriteLine("Current Index= {0}, Domain Title= {1}", index, workerItem.Title);
                            workerItem.WorkerAppDomain.DoWork(msg);                            
                        }
                        channel.BasicAck(deliveryArguments.DeliveryTag, false);
                        count++;
                    }
                }
            }
        }
        
        public void CreateAppDomains()
        {
            if(WorkerItems.Any()) return;

            WorkerItems.Add(CreateWorkerItem(1));
            WorkerItems.Add(CreateWorkerItem(2));
            WorkerItems.Add(CreateWorkerItem(3));

            Console.WriteLine("MessageHandler CreateAppDomain Method. Invoking Test Method....");
            foreach (var workerItem in WorkerItems)
            {
                workerItem.WorkerAppDomain.Test();
            }            
        }

        private static WorkerItem CreateWorkerItem(int id)
        {
            var title = $"Worker Domain {id}";
            var domain = AppDomain.CreateDomain(title);
            var type = typeof (WorkerAppDomain);
            var workerDomain = domain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName) as WorkerAppDomain;

            var workerItem = new WorkerItem
            {
                Title = title,
                AppDomain = domain,
                WorkerAppDomain = workerDomain
            };
            return workerItem;
        }        

        public void Dispose()
        {            
        }

        #region Property

        public List<WorkerItem> WorkerItems { get; set; }
        private static readonly object LockObject = new object();
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private int count;
        
        
        #endregion
    }


    public class WorkerItem
    {
        public string Title { get; set; }
        public AppDomain AppDomain { get; set; }
        public IWorker WorkerAppDomain { get; set; }
    }
}
