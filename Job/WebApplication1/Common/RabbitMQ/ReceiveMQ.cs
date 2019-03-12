using Common;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Web.RabbitMQ
{
    public class ReceiveMQ
    {
        /// <summary>
        /// 接收MQ消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="queueName"></param>
        public static void GetMQ<T>(Func<T,bool> func,string queueName)
        {
                var channel = ConnectionMQ.Connection().CreateModel();
           
                //事件基本消费者
                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

                //接收到消息事件
                consumer.Received += (ch, ea) =>
                {
                    var message = Encoding.UTF8.GetString(ea.Body);
                    var result = false;
                    try
                    {
                        var item = JsonConvert.DeserializeObject<T>(message);
                        result= func(item);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error(ex);
                    }
                    //确认该消息已被消费
                    if (result)
                    channel.BasicAck(ea.DeliveryTag, false);
                };
                //启动消费者 设置为手动应答消息
                channel.BasicConsume(queue:queueName, autoAck:false, consumer: consumer);



            //channel.QueueDeclare(queue: queueName,
            //                 durable: true,
            //                 exclusive: false,
            //                 autoDelete: false,
            //                 arguments: null);

            //    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
            //    var consumer = new EventingBasicConsumer(channel);
            //    consumer.Received += (model, ea) =>
            //    {
            //        var body = ea.Body;
            //        var message = Encoding.UTF8.GetString(body);
            //        log?.Invoke(message);

            //        var item = JsonConvert.DeserializeObject<T>(message);
            //        var result = func(item);
            //        if (result)
            //            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            //    };
            //    channel.BasicConsume(queue: queueName,
            //                         autoAck: false,
            //                         consumer: consumer);
            //}
        }
    }
}
