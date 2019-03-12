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
            using (var channel = ConnectionMQ.Connection().CreateModel())
            {
                //事件基本消费者
                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

                //接收到消息事件
                consumer.Received += (ch, ea) =>
                {
                    var message = Encoding.UTF8.GetString(ea.Body);
                    try
                    {
                        var item = JsonConvert.DeserializeObject<T>(message);
                        func(item);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error(ex);
                    }
                    //确认该消息已被消费
                    channel.BasicAck(ea.DeliveryTag, false);
                };
                //启动消费者 设置为手动应答消息
                channel.BasicConsume(queueName, false, consumer); 
            }
        }
    }
}
