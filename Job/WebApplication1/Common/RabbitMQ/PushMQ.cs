using RabbitMQ.Client;
using RabbitMQ.Client.Framing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Common;

namespace Web.RabbitMQ
{
    public class PushMQ
    {
     /// <summary>
     /// 发送MQ消息
     /// </summary>
     /// <typeparam name="T"></typeparam>
     /// <param name="item"></param>
     /// <param name="queueName"></param>
        public static void SendMQ<T>(T item,string queueName)
        {
            string input = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            using (var channel = ConnectionMQ.Connection().CreateModel())
            {
                //声明一个队列
                channel.QueueDeclare(
                   queue: queueName,
                   durable: true,
                   exclusive: false,
                   autoDelete: false,
                   arguments: null);
                 
                     
                    var sendBytes = Encoding.UTF8.GetBytes(input);
                    var properties = new BasicProperties();
                    properties.DeliveryMode = 2;
                    //发布消息
                    channel.BasicPublish(
                       exchange: "",
                       routingKey: queueName,
                       mandatory: true,
                      basicProperties: properties,
                      body: sendBytes);  
            }
          
           
        }
    }
}
